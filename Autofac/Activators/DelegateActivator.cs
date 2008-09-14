﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofac.Activators
{
    public class DelegateActivator : InstanceActivator, IInstanceActivator
    {
        Func<IComponentContext, IEnumerable<Parameter>, object> _activationFunction;
        
        public DelegateActivator(Type bestGuessImplementationType, Func<IComponentContext, IEnumerable<Parameter>, object> activationFunction)
            : base(bestGuessImplementationType)
        {
            _activationFunction = Enforce.ArgumentNotNull(activationFunction, "activationFunction");
        }

        public object ActivateInstance(IComponentContext context, IEnumerable<Parameter> parameters)
        {
            Enforce.ArgumentNotNull(context, "context");
            Enforce.ArgumentNotNull(parameters, "parameters");

            return _activationFunction(context, parameters);
        }
    }
}