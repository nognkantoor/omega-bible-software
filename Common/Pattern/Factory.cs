using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces.Core.Pattern;

namespace Common.Core.Factory
{
    public class Factory : IFactory
    {
        public virtual TObject Create<TObject>(params object [] parameters)
        {
            return default(TObject);
        }
    }
}
