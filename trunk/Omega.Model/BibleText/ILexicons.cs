using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText
{
    /// <summary>
    /// ILexicons interface represents a bag for lexicon values.
    /// </summary>
    public interface ILexicons : IEnumerable<ILex>
    {
        ILex this[string lexiconName]
        {
            get;
        }

        IEnumerable<ILex> Values
        {
            get;
        }

        void AddLexicon(ILex lex);
        void AddLexicon(string lexiconName, string lexCode);
    }
}
