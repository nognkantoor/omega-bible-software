using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Pattern
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class StartupAttribute : Attribute
    {
        #region Members

        public StartupAttribute()
        {

        }

        public virtual void OnStartup()
        {

        }

        #endregion Members

        #region Initialization on startup

        static StartupAttribute()
        {
            Initialize();
        }

        private static bool _initialized = false;

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

        private static void CheckAssembly(Assembly assembly)
        {
            Type[] typesInAssembly = assembly.GetTypes();
            for (int i = 0; i < typesInAssembly.Length; i++)
            {
                Type type = typesInAssembly[i];
                object[] attributes = type.GetAttributes<StartupAttribute>();
                if (attributes != null)
                {
                    for (int k = 0; k < attributes.Length; k++)
                    {
                        ((StartupAttribute)attributes[k]).OnStartup();
                    }
                }
            }
        }

        private static void OnAssemblyLoading(object sender, AssemblyLoadEventArgs args)
        {
            CheckAssembly(args.LoadedAssembly);
        }

        #endregion Initialization on startup
    }
}
