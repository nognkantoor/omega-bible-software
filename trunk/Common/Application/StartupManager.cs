using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Application
{
    /// <summary>
    /// Controls the startup initialization process for OnStartupAttribute's.
    /// </summary>
    public class StartupManager
    {    
        #region Initialization on startup

        /// <summary>
        /// Holds a value indicating if the initialization was made.
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// Initializes all startup attributes in current assembly domain.
        /// </summary>
        public static void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                for (int i = 0; i < currentAssemblies.Length; i++)
                {
                    CheckAssembly(currentAssemblies[i]);
                }

                AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(OnAssemblyLoading);
            }
        }

        /// <summary>
        /// Checks an assembly for class types having a StartupAttribute or derived classes.
        /// </summary>
        /// <param name="assembly">Assembly to check.</param>
        private static void CheckAssembly(Assembly assembly)
        {
            Type[] typesInAssembly = assembly.GetTypes();
            for (int i = 0; i < typesInAssembly.Length; i++)
            {
                Type type = typesInAssembly[i];
                var attributes = type.GetAttributes<OnStartupAttribute>();
                if (attributes != null)
                {
                    foreach(OnStartupAttribute attribute in attributes)
                    {
                        attribute.OnStartup(type);
                    }
                }
            }
        }

        /// <summary>
        /// Handles all assemblies that were not yet loaded on startup, but are loaded later.
        /// </summary>
        /// <param name="sender">Sender assembly.</param>
        /// <param name="args">Arguments for the assembly loading.</param>
        private static void OnAssemblyLoading(object sender, AssemblyLoadEventArgs args)
        {
            CheckAssembly(args.LoadedAssembly);
        }

        #endregion Initialization on startup
        
    }
}
