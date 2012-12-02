using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Model.Bibles;

namespace Omega.Model.BibleText
{
    public interface IBibleFactory
    {
        IBibleProvider CreateProvider(IBibleDescriptor descriptor);
    }
}
