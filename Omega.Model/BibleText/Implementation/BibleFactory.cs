using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Model.Bibles.Implementation;
using Common.Core.Pattern;

namespace Omega.Model.BibleText.Implementation
{
    [RegisterInFactory(typeof(IBibleFactory))]
    public class BibleFactory : IBibleFactory
    {
        public IBibleProvider CreateProvider(Bibles.IBibleDescriptor descriptor)
        {
            IBibleProvider provider = null;
            if (descriptor is FileBibleDescriptor)
            {
                provider = new FileBibleProvider();
            }
            else if (descriptor is DatabaseBibleDescriptor)
            {
                provider = new DatabaseBibleProvider();
            }
            if (provider != null)
            {
                provider.Initialize(descriptor);
            }
            return provider;
        }
    }
}
