using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Test
{
    public class Test
    {
        [STAThread]
        public static int Main(string[] args)
        {
            return NUnit.Gui.AppEntry.Main(new string [] { "Common.Core.Test.exe" });
        }
    }
}
