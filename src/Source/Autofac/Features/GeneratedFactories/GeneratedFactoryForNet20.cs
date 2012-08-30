using System.Collections.Generic;
using Autofac.Core;

namespace Autofac.Features.GeneratedFactories
{
	/// <summary>
	/// Class that holds the generated factory method and all required data
	/// </summary>
	public class GeneratedFactoryForNet20
	{
		/// <summary>
		/// Constructs an object
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="service">Service</param>
		/// <param name="productRegistration">Product registration</param>
		public GeneratedFactoryForNet20(
			IComponentContext context, Service service, IComponentRegistration productRegistration)
		{
			_context = context;
			_service = service;
			_productRegistration = productRegistration;
		}

		/// <summary>
		/// Resolves the component
		/// </summary>
		/// <param name="parameters">Parameters</param>
		/// <returns>Resolved component</returns>
		public object InternalCreate(IEnumerable<Parameter> parameters)
		{
			object result;

			if (_service != null)
				result = _context.ResolveService(_service, parameters);
			else
				result = _context.ResolveComponent(_productRegistration, parameters);

			return result;
		}

		private readonly IComponentContext _context;
		private readonly Service _service;
		private readonly IComponentRegistration _productRegistration;
	}
}