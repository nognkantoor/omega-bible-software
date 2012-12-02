using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.Bibles
{
    public interface IBibleDescriptor
    {
        string BibleId
        {
            get;
        }

        string BibleSource
        {
            get;
        }
    }
}
