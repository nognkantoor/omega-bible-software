using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Omega.Tools.Utilities;
using Common.Core.Pattern;

namespace Omega.Tools.BibleParser
{
    public partial class BiblePageParser : Common.Controls.Forms.ExtendedForm
    {
        public BiblePageParser()
        {
            InitializeComponent();
        }

        //protected override Common.Interfaces.Core.Translation.ITranslator GetTranslator()
        //{
        //    return Singleton<Context>.Instance.Localization;
        //}
    }
}
