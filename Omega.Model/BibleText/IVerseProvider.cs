using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Model.Bibles;

namespace Omega.Model.BibleText
{
    /// <summary>
    /// Defines a provider interface, that can work either with files or databases or
    /// any other source.
    /// </summary>
    public interface IBibleProvider
    {
        IList<IVerse> GetVerses(string book, int chapter);
        IList<IVerse> GetVerses(string book, int chapter, int fromVerse, int toVerse);
        void Initialize(IBibleDescriptor descriptor);
    }
}
