using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText.Implementation
{
    public class Verse : IVerse
    {
        public Verse()
        {
            Lexicons = new Lexicons();
            Words = new List<IWord>();
        }

        public IList<IWord> Words
        {
            get;
            protected set;
        }

        public ILexicons Lexicons
        {
            get;
            protected set;
        }
    }
}
