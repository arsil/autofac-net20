﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Autofac.Integration.Wcf {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AutofacHostFactoryResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AutofacHostFactoryResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Autofac.Integration.Wcf.AutofacHostFactoryResources", typeof(AutofacHostFactoryResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The AutofacServiceHost.Container static property must be set before services can be instantiated..
        /// </summary>
        internal static string ContainerIsNull {
            get {
                return ResourceManager.GetString("ContainerIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service &apos;{0}&apos; is registered in such a way that its implementation type could not be derived. This is required for WCF integration. Consider using a reflection-based registration or an expression-based registration where the return value is a class type. The current default registration is &apos;{1}&apos;..
        /// </summary>
        internal static string ImplementationTypeUnknown {
            get {
                return ResourceManager.GetString("ImplementationTypeUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The WCF service &apos;{0}&apos; has an InstanceContextMode of Single and is not registered as SingleInstance with the Autofac container..
        /// </summary>
        internal static string ServiceMustBeSingleInstance {
            get {
                return ResourceManager.GetString("ServiceMustBeSingleInstance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The WCF service &apos;{0}&apos; does not have an InstanceContextMode of Single and is registered as SingleInstance with the Autofac container..
        /// </summary>
        internal static string ServiceMustNotBeSingleInstance {
            get {
                return ResourceManager.GetString("ServiceMustNotBeSingleInstance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service &apos;{0}&apos; configured for WCF is not registered with the Autofac container..
        /// </summary>
        internal static string ServiceNotRegistered {
            get {
                return ResourceManager.GetString("ServiceNotRegistered", resourceCulture);
            }
        }
    }
}
