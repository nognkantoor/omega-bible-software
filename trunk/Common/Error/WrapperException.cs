using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Error
{
    /// <summary>
    /// Wrapps an exception for more specific indentification of the exception by
    /// a Symbol enumeration.
    /// </summary>
    public class WrapperException : Exception
    {
        #region Static

        /// <summary>
        /// Throws the Exception pointed by the Exceptions symbol.
        /// </summary>
        /// <param name="code">Exceptions code to throw.</param>
        public static void Throw(Exceptions code)
        {
            throw new WrapperException(code, null);
        }

        /// <summary>
        /// Throws the Exception pointed by the Exceptions symbol.
        /// </summary>
        /// <param name="code">Exceptions code to throw.</param>
        /// <param name="exception">Inner exception.</param>
        /// <param name="parameters">Optional parameters for the developer message.</param>
        public static void Throw(Exceptions code, Exception exception, params object [] parameters)
        {
            throw new WrapperException(code, exception, parameters);
        }

        /// <summary>
        /// Throws the Exception pointed by the Exceptions symbol.
        /// </summary>
        /// <param name="code">Exceptions code to throw.</param>
        /// <param name="parameters">Optional parameters for the developer message.</param>
        public static void Throw(Exceptions code, params object[] parameters)
        {
            throw new WrapperException(code, null, parameters);
        }

        /// <summary>
        /// Formats the string with the given parameters.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="parameters">Parameters for formating.</param>
        /// <returns>Formated result string.</returns>
        protected static string Format(string format, params object[] parameters)
        {
            try
            {
                if (parameters == null || parameters.Length == 0) return format;
                return string.Format(format, parameters);
            }
            catch (Exception ex)
            {
                return format;
            }
        }

        #endregion Static

        #region Members

        /// <summary>
        /// Gets or sets the parameters for the user message.
        /// </summary>
        protected object[] UserMessageParams
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the WrapperException class
        /// with its code and inner exception.
        /// </summary>
        /// <param name="code">Exceptions code that precisely describes the exception.</param>
        /// <param name="exception">Inner exception.</param>
        /// <param name="developerParams">Parameters for the developer message.</param>
        public WrapperException(Exceptions code, Exception exception, params object[] developerParams)
            : base(Format(code.Description, developerParams), exception)
        {
            if (code == null) throw new NullReferenceException("Code cannot be null");
            Code = code;
        }

        /// <summary>
        /// Sets the format parameters for the user message.
        /// </summary>
        /// <param name="parameters">Parameters used in the format string of the user message.</param>
        /// <returns>Itself for chain invocation.</returns>
        public WrapperException SetUserParameters(params object[] parameters)
        {
            UserMessageParams = parameters;
            return this;
        }

        /// <summary>
        /// Gets the final, formated developer exception message
        /// with all detailed information about the exception.
        /// </summary>
        public string DeveloperMessage
        {
            get
            {
                return Message;
            }
        }

        /// <summary>
        /// Gets the final, formated user message, which should
        /// describe the exception with an eye friendly text information
        /// without high level technical details.
        /// </summary>
        public string UserMessage
        {
            get
            {
                if (UserMessageParams != null && UserMessageParams.Length > 0)
                {
                    return Format(Code.UserDescription, UserMessageParams);
                }
                return Code.UserDescription;
            }
        }

        /// <summary>
        /// Gets the exception code which identifies the
        /// exception precisely.
        /// </summary>
        public Exceptions Code
        {
            get;
            protected set;
        }

        #endregion Members
    }
}
