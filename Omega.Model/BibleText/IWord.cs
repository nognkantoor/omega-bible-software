using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText
{
    /// <summary>
    /// IWord interface represents one word part of a verse. The word
    /// can have lexicon references attached.
    /// </summary>
    public interface IWord
    {
        /// <summary>
        /// Gets the content of the word.
        /// </summary>
        string Content
        {
            get;
        }

        /// <summary>
        /// Gets the lexicon collection.
        /// </summary>
        ILexicons Lexicons
        {
            get;
        }
    }
}
