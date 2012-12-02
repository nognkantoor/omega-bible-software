using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText
{
    /// <summary>
    /// ILex interface represents an atomic lexicon part, which consists out of a lexicon code and 
    /// dictionary code identifier
    /// </summary>
    public interface ILex
    {
        /// <summary>
        /// Gets the lexicon indentifier.
        /// </summary>
        string Lexicon
        {
            get;
        }

        /// <summary>
        /// Gets the code of the word in the lexicon.
        /// </summary>
        string Code
        {
            get;
        }
    }
}
