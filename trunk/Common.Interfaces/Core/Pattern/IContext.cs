using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces.Core.Translation;

namespace Common.Interfaces.Core.Pattern
{
    public interface IContext
    {
        IFactory Factory
        {
            get;
        }

        ITranslator Localization
        {
            get;
        }
    }
}
