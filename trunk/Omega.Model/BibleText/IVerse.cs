using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText
{
    /// <summary>
    /// IVerse interface represents one, particular bible verse with
    /// it's words and additional list of lexicon references (for the whole verse,
    /// not for words - they have their own lexicon references).
    /// </summary>
    public interface IVerse
    {
        IList<IWord> Words
        {
            get;
        }

        ILexicons Lexicons
        {
            get;
        }
    }
}
