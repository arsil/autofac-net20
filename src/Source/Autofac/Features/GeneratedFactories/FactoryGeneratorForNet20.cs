using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Autofac.Core;
using Autofac.Util;

namespace Autofac.Features.GeneratedFactories
{
	class FactoryGenerator
	{
		/// <summary>
		/// Create a factory generator.
		/// </summary>
		/// <param name="service">The service that will be activated in
		/// order to create the products of the factory.</param>
		/// <param name="delegateType">The delegate to provide as a factory.</param>
		/// <param name="parameterMapping">The parameter mapping mode to use.</param>
		public FactoryGenerator(Type delegateType, Service service, ParameterMapping parameterMapping)
		{
			if (service == null) throw new ArgumentNullException("service");
			Enforce.ArgumentTypeIsFunction(delegateType);

			_service = service;
			_delegateType = delegateType;
			_parameterMapping = GetParameterMapping(delegateType, parameterMapping);
		}

		/// <summary>
		/// Create a factory generator.
		/// </summary>
		/// <param name="productRegistration">The component that will be activated in
		/// order to create the products of the factory.</param>
		/// <param name="delegateType">The delegate to provide as a factory.</param>
		/// <param name="parameterMapping">The parameter mapping mode to use.</param>
		public FactoryGenerator(Type delegateType, IComponentRegistration productRegistration, ParameterMapping parameterMapping)
		{
			if (productRegistration == null) throw new ArgumentNullException("productRegistration");
			Enforce.ArgumentTypeIsFunction(delegateType);

			_productRegistration = productRegistration;
			_delegateType = delegateType;
			_parameterMapping = GetParameterMapping(delegateType, parameterMapping);
		}

		/// <summary>
		/// Generates a factory delegate that closes over the provided context.
		/// </summary>
		/// <param name="context">The context in which the factory will be used.</param>
		/// <param name="parameters">Parameters provided to the resolve call for the factory itself.</param>
		/// <returns>A factory delegate that will work within the context.</returns>
		public Delegate GenerateFactory(IComponentContext context, IEnumerable<Parameter> parameters)
		{
			if (context == null) throw new ArgumentNullException("context");
			if (parameters == null) throw new ArgumentNullException("parameters");

			return GenerateMethod(context.Resolve<ILifetimeScope>(), parameters);
		}

		/// <summary>
		/// Generates a factory delegate that closes over the provided context.
		/// </summary>
		/// <param name="context">The context in which the factory will be used.</param>
		/// <param name="parameters">Parameters provided to the resolve call for the factory itself.</param>
		/// <returns>A factory delegate that will work within the context.</returns>
		public TDelegate GenerateFactory<TDelegate>(IComponentContext context, IEnumerable<Parameter> parameters)
			where TDelegate : class
		{
			return (TDelegate)(object)GenerateFactory(context, parameters);
		}

		private Delegate GenerateMethod(IComponentContext context, IEnumerable<Parameter> parameters)
		{
			// signature of the factory method
			var invoke = _delegateType.GetMethod("Invoke");

			// retrive parameters
			var creatorParams = invoke
				.GetParameters()
				.Select(pi => pi.ParameterType)
				.ToList();

			// instance method - have to add 'this'
			creatorParams.Insert(0, typeof(GeneratedFactoryForNet20));

			var dynamicMethod = new DynamicMethod(
				"",
				invoke.ReturnType,
				creatorParams.ToArray(),
				typeof (GeneratedFactoryForNet20));

			var il = dynamicMethod.GetILGenerator(256);

			// local storage for List<>
			il.DeclareLocal(TypeOfList);

			// new List<Parameter>
			il.Emit(OpCodes.Newobj, MiListConstructor);
			il.Emit(OpCodes.Stloc_0);

			// for each parameter
			int paramIndex = 1;
			foreach (var par in invoke.GetParameters())
			{
				ConstructorInfo currentConstructorInfo;

				il.Emit(OpCodes.Ldloc_0); // List

				if (_parameterMapping == ParameterMapping.ByType)
				{
					// load parameter
					il.Emit(OpCodes.Ldarg_S, paramIndex);

					// box if value type
					if (par.ParameterType.IsValueType)
						il.Emit(OpCodes.Box, par.ParameterType);

					// call GetType
					il.EmitCall(OpCodes.Callvirt, MiObjectGetType, null);

					currentConstructorInfo = MiTypedParameterConstructor;
				}
				else if (_parameterMapping == ParameterMapping.ByPosition)
				{
					// push parameter index
					il.Emit(OpCodes.Ldc_I4, paramIndex - 1);
					currentConstructorInfo = MiPositionalParameterConstructor;
				}
				else
				{
					// push parameter name
					il.Emit(OpCodes.Ldstr, par.Name);
					currentConstructorInfo = MiNamedParameterConstructor;
				}

				// load parameter
				il.Emit(OpCodes.Ldarg_S, paramIndex);

				// box if value type
				if (par.ParameterType.IsValueType)
					il.Emit(OpCodes.Box, par.ParameterType);

				// call constructor
				il.Emit(OpCodes.Newobj, currentConstructorInfo);

				// add object to the list
				il.EmitCall(OpCodes.Callvirt, MiListAdd, null);

				++paramIndex;
			}

			// this
			il.Emit(OpCodes.Ldarg_0);
			// parameters list
			il.Emit(OpCodes.Ldloc_0);

			// call InternalCreate
			il.EmitCall(OpCodes.Call, MiHolderInternalCreate, null);

			// if return type is value type - unbox
			// otherwise cast if needed
			if (invoke.ReturnType.IsValueType)
				il.Emit(OpCodes.Unbox_Any, invoke.ReturnType);
			else if (invoke.ReturnType != typeof(object))
				il.Emit(OpCodes.Castclass, invoke.ReturnType);

			// return
			il.Emit(OpCodes.Ret);

			// create holder for the new method
			var resultObj = new GeneratedFactoryForNet20(context, _service, _productRegistration);

			// return delegate
			return dynamicMethod.CreateDelegate(_delegateType, resultObj);
		}

		static ParameterMapping GetParameterMapping(Type delegateType, ParameterMapping configuredParameterMapping)
		{
			if (configuredParameterMapping == ParameterMapping.Adaptive)
				return delegateType.Name.StartsWith("Func`") ? ParameterMapping.ByType : ParameterMapping.ByName;

			return configuredParameterMapping;
		}

		readonly Service _service;
		readonly IComponentRegistration _productRegistration;
		readonly Type _delegateType;
		readonly ParameterMapping _parameterMapping;

		// static data

		private static readonly MethodInfo MiObjectGetType = typeof(object).GetMethod("GetType");

		private static readonly Type TypeOfList = typeof(List<Parameter>);
		private static readonly MethodInfo MiListAdd = TypeOfList.GetMethod("Add");
		private static readonly ConstructorInfo MiListConstructor = TypeOfList.GetConstructor(new Type[] { });

		private static readonly ConstructorInfo MiTypedParameterConstructor
			= typeof(TypedParameter).GetConstructor(new[] { typeof(Type), typeof(object) });
		private static readonly ConstructorInfo MiNamedParameterConstructor
			= typeof(NamedParameter).GetConstructor(new[] { typeof(string), typeof(object) });
		private static readonly ConstructorInfo MiPositionalParameterConstructor
			= typeof(PositionalParameter).GetConstructor(new[] { typeof(int), typeof(object) });

		private static readonly MethodInfo MiHolderInternalCreate
			= typeof(GeneratedFactoryForNet20).GetMethod("InternalCreate");
	}
}
