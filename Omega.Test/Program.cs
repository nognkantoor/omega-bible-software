using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Test
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            return NUnit.Gui.AppEntry.Main(new string[] { "Omega.Test.exe" });
        }
    }
}
