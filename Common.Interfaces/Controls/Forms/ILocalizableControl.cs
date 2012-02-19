using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Controls.Forms
{
    public interface ILocalizableControl
    {
        Common.Interfaces.Core.Translation.ITranslator Translator
        {
            get;
            set;
        }
    }
}
