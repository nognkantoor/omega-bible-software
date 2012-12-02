using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;

namespace Common.Core.Application
{
    /// <summary>
    /// Program class holds basic program/application data for
    /// a global use.
    /// </summary>
    public class Program
    {
        public static bool IsRelease
        {
            get
            {
#if DEBUG
                return false;
#else 
                return true;
#endif
            }
        }


        public static void Initialize()
        {
            if(ExecutablePath != null) return;

            ExecutablePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ApplicationDataPath = ExecutablePath;
            DefaultLogger = LogManager.GetLogger(typeof(Program));
        }

        static Program()
        {
            Initialize();
        }

        public static ILog DefaultLogger
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the path to the executable.
        /// </summary>
        public static string ExecutablePath
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the application data path. Default is the same as ExecutablePath, but can be changed.
        /// </summary>
        public static string ApplicationDataPath
        {
            get;
            set;
        }
    }
}
