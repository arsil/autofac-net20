﻿// This software is part of the Autofac IoC container
// Copyright (c) 2010 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Autofac.Core;
using Autofac.Util;

namespace Autofac.Builder
{
    /// <summary>
    /// Registration style for individual components.
    /// </summary>
    public class SingleRegistrationStyle
    {
        Guid _id = Guid.NewGuid();

        readonly ICollection<EventHandler<ComponentRegisteredEventArgs>>
            _registeredHandlers = new List<EventHandler<ComponentRegisteredEventArgs>>();

        bool _preserveDefaults;

        readonly Type _defaultServiceType;

        /// <summary>
        /// Create a new SingleRegistrationStyle.
        /// </summary>
        /// <param name="defaultServiceType">The type that will be used as the default service if
        /// no other services are configured.</param>
        public SingleRegistrationStyle(Type defaultServiceType)
        {
            _defaultServiceType = Enforce.ArgumentNotNull(defaultServiceType, "defaultServiceType");
        }

        /// <summary>
        /// The id used for the registration.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Handlers to notify of the component registration event.
        /// </summary>
        public ICollection<EventHandler<ComponentRegisteredEventArgs>> RegisteredHandlers { get { return _registeredHandlers; } }

        /// <summary>
        /// By default, new registrations override existing registrations as defaults.
        /// If set to true, new registrations will not change existing defaults.
        /// </summary>
        public bool PreserveDefaults
        {
            get { return _preserveDefaults; }
            set { _preserveDefaults = value; }
        }

        /// <summary>
        /// The component upon which this registration is based.
        /// </summary>
        public IComponentRegistration Target { get; set; }

        /// <summary>
        /// The type that will be used as the default service if
        /// no other services are configured.
        /// </summary>
        public Type DefaultServiceType
        {
            get { return _defaultServiceType; }
        }
    }
}
