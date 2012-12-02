using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces.Core.Pattern;
using Common.Core.Collections;
using System.Reflection;
using Common.Core.Error;

namespace Common.Core.Pattern
{
    /// <summary>
    /// The Factory class gives the basic implementation of a factory pattern.
    /// </summary>
    public class Factory : IFactory
    {
        #region Static members

        /// <summary>
        /// Organizes the exceptions thrown by the factory implementation.
        /// </summary>
        public class FactoryExceptions
        {
            /// <summary>
            /// This exception means that a specified type {0} doesn't have 
            /// an apriopriate constructor with given list of types {1}.
            /// User description has no parameters.
            /// </summary>
            public static readonly Exceptions NoConstructorFoundException = Exceptions.Get("NoConstructorFound")
                .Set("Type {0} doesn't have any constructor with types {1}")
                .SetUserDescription("Error on initialization.");

            /// <summary>
            /// This exception means that the given base type {0} doesn't.
            /// Have an implementation registered.
            /// User description has no parameters.
            /// </summary>
            public static readonly Exceptions NoRegisteredImplementationException = Exceptions.Get("NoRegisteredImplementation")
                .Set("Base type {0} doesn't have any registered implementation")
                .SetUserDescription("Error on initialization.");

            /// <summary>
            /// This exception means that the given implementation type {0} doesn't.
            /// derive from the base class {1}.
            /// User description has no parameters.
            /// </summary>
            public static readonly Exceptions NotDerivedFromException = Exceptions.Get("NotDerivedFrom")
                .Set("The type {0} isn't an derived or implementing class from {1}")
                .SetUserDescription("Error on initialization");

            /// <summary>
            /// This exception means that the registered implementation
            /// has been given null types as parameters.
            /// User description has no parameters.
            /// </summary>
            public static readonly Exceptions MustProvideNotNullTypesException = Exceptions.Get("MustProvideNotNullTypes")
                .Set("Must provide not null types for registering in factory")
                .SetUserDescription("Error on initialization");

            /// <summary>
            /// This exceptions means that the base type {0} has already
            /// a registered implementation {1}.
            /// User description has no parameters.
            /// </summary>
            public static readonly Exceptions AlreadyRegisteredException = Exceptions.Get("AlreadyRegistered")
                .Set("The type {0} has already a registered implementation: {1}")
                .SetUserDescription("Error on initialization");

        }

        /// <summary>
        /// Holds the static singleton instance of the factory.
        /// </summary>
        private static IFactory _instance;

        /// <summary>
        /// Gets the instance of the current main factory.
        /// </summary>
        public static IFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Factory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Sets an implementation of a factory for the main factory instance.
        /// </summary>
        /// <param name="factory">Factory instance which will substitute the current main factory.</param>
        public static void SetInstance(IFactory factory)
        {
            _instance = factory;
        } 

        #endregion Static members

        #region Custom

        /// <summary>
        /// Holds the dictionary of registered factory implementations.
        /// </summary>
        private Dictionary<Type, Tuple<Type, Func<object[], object>>> _registered = new Dictionary<Type, Tuple<Type, Func<object[], object>>>();

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be invoked
        /// when a registration of a new implementation occurs and will override
        /// an existing one.
        /// </summary>
        public bool IsExceptionOnRegisterOverride
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the function for constructor.
        /// </summary>
        /// <param name="baseType">Base type for the implementation.</param>
        /// <param name="implementation">Implementation type.</param>
        /// <param name="exceptionOnError">Exception thrown on no constructor.</param>
        /// <param name="parameters">Parameters for the constructor.</param>
        /// <returns>A constructor.</returns>
        private Func<object[], object> GetConstructor(Type baseType, Type implementation, Exception exceptionOnError, object[] parameters)
        {
            if (!baseType.IsAssignableFrom(implementation))
            {
                throw FactoryExceptions.NotDerivedFromException
                        .Now(implementation.Name, baseType.Name);
            }

            Type[] types = parameters as Type[];
            if (types == null && parameters != null)
            {
                types = new Type[parameters.Length];
                int index = 0;
                parameters.Each(p => types[index] = p.GetType());
            }

            ConstructorInfo constructor = null;

            constructor = implementation
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public,
                null, types, null);

            if (constructor == null)
            {
                throw exceptionOnError;
                    
            }

            return c => constructor.Invoke(c);
        }

        #endregion Custom

        #region Implementation

        /// <summary>
        /// Registers an implementation for the TBase class.
        /// </summary>
        /// <typeparam name="TBase">The base type for registering.</typeparam>
        /// <typeparam name="TImplementation">The implementation of the type.</typeparam>
        /// <param name="constructorParamTypes">Types for the constructor.</param>
        public virtual void Register<TBase, TImplementation>(params Type[] constructorParamTypes)
        {
            Type baseType = typeof(TBase);
            Type implementationType = typeof(TImplementation);
            Register(baseType, implementationType, constructorParamTypes);
        }

        /// <summary>
        /// Registers an implementation for the base class.
        /// </summary>
        /// <param name="baseType">The base type for registering.</param>
        /// <param name="implementationType">The implementation of the type.</param>
        /// <param name="constructorParamTypes">Types for the constructor.</param>
        public virtual void Register(Type baseType, Type implementationType, params Type[] constructorParamTypes)
        {
            if (baseType == null || implementationType == null)
            {
                throw FactoryExceptions.NotDerivedFromException.Now();
            }

            if (IsExceptionOnRegisterOverride && _registered.ContainsKey(baseType))
            {
                throw FactoryExceptions.AlreadyRegisteredException
                       .Now(baseType.Name, _registered[baseType].Item1.Name);
            }

            Exception onError = FactoryExceptions.NoConstructorFoundException
                .Now(implementationType.Name,
                    string.Join(", ", constructorParamTypes != null ? 
                        constructorParamTypes.Select(o => o != null ? o.GetType().Name : "-") : new string [] { "-" } ));


            var constructor = GetConstructor(baseType, implementationType, onError, constructorParamTypes);

            _registered[baseType] = Tuple.Create(implementationType, constructor);
        }

        /// <summary>
        /// Registers an implementation for the base class.
        /// <param name="constructor">A function creating the implementation.</param>
        /// <typeparam name="TBase">Base class for implementing.</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// </summary>
        public virtual void Register<TBase,TImplementation>(Func<TImplementation> constructor) where TImplementation : TBase
        {
            _registered[typeof(TBase)] = Tuple.Create(typeof(TImplementation), new Func<object[],object>( (p) => constructor()) );
        }

        /// <summary>
        /// Creates an instance of the given type,
        /// by getting the registered in factory implementation.
        /// </summary>
        /// <typeparam name="TObject">The type of the base class.</typeparam>
        /// <param name="parameters">Additional parameters for the constructor.</param>
        /// <returns>The implementation of the given base type.</returns>
        public virtual TObject Create<TObject>(params object[] parameters)
        {
            Type baseType = typeof(TObject);
            object[] p = parameters != null && parameters.Length == 0 ? null : parameters;
            Tuple<Type, Func<object[], object>> constructor = _registered.GetForKey(baseType);

            Exception onError = FactoryExceptions.NoRegisteredImplementationException
                .Now(baseType.Name);

            if (constructor == null)
            {
                if (!baseType.IsAbstract && !baseType.IsInterface)
                {
                    var create = GetConstructor(baseType,baseType,onError, parameters);
                    if (create != null)
                        return (TObject)create(parameters);
                }
            }
            if (constructor == null)
            {
                throw onError;
            }

            return (TObject)constructor.Item2(p);
        }

        #endregion Implementation
    }
}
