using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.Pattern;

namespace Common.Core.Error
{
    /// <summary>
    /// The Exceptions class is a Symbol pool for a special exception
    /// descriptor, that helps in identifying the exception couse on the
    /// developer level as good as on the user level.
    /// </summary>
    /// <remarks>
    /// Creating an Exceptions symbol can be done by the Pool.Get(string) method
    /// and the new Exceptions object can get its description by Set(string) 
    /// method which is a detailed description for developers, and
    /// SetUserDescription(string) which is the eye-friendly message
    /// for an end user. The given messages can be also used
    /// as format strings, which get their parameters in the moment
    /// when it's thrown.
    /// </remarks>
    public class Exceptions : Symbol
    {
        /// <summary>
        /// Initializes a new instance of the Exceptions symbol. 
        /// This constructor is private, becouse the symbol
        /// can be created only through the Pool property.
        /// </summary>
        /// <param name="prototype">Base implementation of Symbol.</param>
        private Exceptions(Symbol prototype) : base(prototype) 
        {
        }

        /// <summary>
        /// Gets the pool of Exceptions symbols.
        /// </summary>
        public static new readonly SymbolPool<Exceptions> Pool
                                = new SymbolPool<Exceptions>(p => new Exceptions(p));

        /// <summary>
        /// Get the exception with the given name. 
        /// This is an alias to Pool.Get(string).
        /// </summary>
        /// <param name="name">Name of the symbol.</param>
        /// <returns>The Exceptions symbol.</returns>
        public static Exceptions Get(string name)
        {
            return Pool.Get(name);
        }

        /// <summary>
        /// Sets the description for the developer
        /// that will have a detailed text about the 
        /// exception.
        /// </summary>
        /// <param name="description">The description. It can be a format
        /// string, which gives its parameters when it's thrown.</param>
        /// <returns>Exceptions object itself for chain invocation.</returns>
        public Exceptions Set(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        /// Sets the user description for the end
        /// user.
        /// </summary>
        /// <param name="description">The description. It can be a format
        /// string, which gives its parameters when it's thrown.</param>
        /// <returns>Exceptions object itself for chain invocation.</returns>
        public Exceptions SetUserDescription(string description)
        {
            UserDescription = description;
            return this;
        }

        /// <summary>
        /// Gets the developer message description, or format string.
        /// </summary>
        public string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the user message description, or format string.
        /// </summary>
        public string UserDescription
        {
            get;
            protected set;
        }

        /// <summary>
        /// Creates a WrapperException with this Exceptions code.
        /// </summary>
        /// <remarks>This is for a fluent use in code
        /// like <code>throw ExceptionsSymbol.Now();</code>
        /// </remarks>
        /// <returns>A WrapperException.</returns>
        public WrapperException Now()
        {
            var ex = Factory.Instance.Create<WrapperException>(this);
            return ex;
        }

        /// <summary>
        /// Creates a WrapperException with this Exceptions code.
        /// </summary>
        /// <remarks>This is for a fluent use in code
        /// like <code>throw ExceptionsSymbol.Now();</code>
        /// </remarks>
        /// <returns>A WrapperException.</returns>
        public WrapperException Now(params object [] parameters)
        {
            var ex = Factory.Instance.Create<WrapperException>(this, parameters);
            return ex;
        }
    }
}
