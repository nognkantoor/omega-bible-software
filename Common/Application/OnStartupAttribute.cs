using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Application
{
    /// <summary>
    /// StartupAttribute class is a base class for attributes that should be 
    /// invoked at startup of the application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]  
    public abstract class OnStartupAttribute : Attribute
    {
        #region Members

        /// <summary>
        /// Initializes a new instance of the StartupAttribute class.
        /// </summary>
        public OnStartupAttribute()
        {
            
        }

        /// <summary>
        /// Processes actions to be made on application startup.
        /// </summary>
        /// <param name="ownerClass">Class that has the StartupAttribute applied.</param>
        public abstract void OnStartup(Type ownerClass);
        
        #endregion Members
    }
}
