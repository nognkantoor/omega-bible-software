using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.Application;

namespace Omega.Main.Configuration
{
    public class LoggerHelper : log4net.Util.PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            string folder = ".\\";
            if (Program.IsRelease)
            {
                folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                folder =  folder + "\\Omega\\";
            }

            writer.Write(folder);
        }
    }
}
