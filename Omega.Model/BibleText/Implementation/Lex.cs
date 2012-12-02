using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText.Implementation
{
    /// <summary>
    /// Lex class represents an atomic lexicon part, which consists out of a lexicon code and 
    /// dictionary code identifier
    /// </summary>
    public class Lex : ILex
    {
        public Lex()
        { 
        }

        public Lex(string lexicon, string code)
        {
            Lexicon = lexicon;
            Code = code;
        }

        public string Lexicon
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }
    }
}
