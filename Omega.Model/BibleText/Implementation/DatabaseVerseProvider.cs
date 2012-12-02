using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText.Implementation
{
    public class DatabaseBibleProvider: BaseBibleProvider
    {
        protected override IList<IVerse> GetVersesOverride(string book, int chapter)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(Bibles.IBibleDescriptor descriptor)
        {
            throw new NotImplementedException();
        }
    }
}
