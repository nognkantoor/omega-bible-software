using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Pattern
{
    /// <summary>
    /// Implements the singleton pattern.
    /// </summary>
    /// <typeparam name="TSingleton">Any type for creating a singleton with a private
    /// parametless constructor.</typeparam>
    public class Singleton<TSingleton> where TSingleton : class
    {
        /// <summary>
        /// Object for locking any other instance creation.
        /// </summary>
        static object SyncRoot = new object();

        /// <summary>
        /// Instance of type TSingleton
        /// </summary>
        static TSingleton instance;

        /// <summary>
        /// Gets the singleton instance of a class of type TSingleton.
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            ConstructorInfo constructor = typeof(TSingleton)
                                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
                            if (constructor == null) 
                            { 
                                throw new InvalidOperationException("Class must contain a private constructor"); 
                            }
                            instance = (TSingleton)constructor.Invoke(null);
                        }
                    }
                }
                return instance;
            }
        }
    }
}
