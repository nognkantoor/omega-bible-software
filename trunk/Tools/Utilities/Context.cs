using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces.Core.Translation;
using Common.Core.Pattern;
using Common.Interfaces.Core.Pattern;

namespace Omega.Tools.Utilities
{
    public class Context : Common.Interfaces.Core.Pattern.IContext
    {
        private Context()
        {
            Factory =  Singleton<Utilities.Factory>.Instance;
            Localization = Factory.Create<ITranslator>();
#if DEBUG
            Localization.IsGatheringMissingKeys = true;
#endif
        }

        public IFactory Factory
        {
            get;
            protected set;
        }

        public ITranslator Localization
        {
            get;
            protected set;
        }
    }
}
