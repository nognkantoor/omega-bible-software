using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText.Implementation
{
    public class Word : IWord
    {
        public Word()
        {
            Lexicons = new Lexicons();
        }

        public string Content
        {
            get;
            set;
        }

        public ILexicons Lexicons
        {
            get;
            protected set;
        }
    }
}
