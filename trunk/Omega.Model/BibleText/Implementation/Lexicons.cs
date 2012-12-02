using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.BibleText.Implementation
{
    public class Lexicons : ILexicons
    {
        private Dictionary<string, ILex> _lexes = new Dictionary<string,ILex>();
        public Lexicons()
        { 
        }

        public ILex this[string lexiconName]
        {
            get
            {
                ILex lex = null;
                _lexes.TryGetValue(lexiconName, out lex);
                return lex;
            }
        }

        public IEnumerable<ILex> Values
        {
            get
            {
                return _lexes.Values;
            }
        }

        public void AddLexicon(ILex lex)
        {
            _lexes[lex.Lexicon] = lex;
        }

        public void AddLexicon(string lexiconName, string lexCode)
        {
            _lexes[lexiconName] = new Lex(lexiconName, lexCode);
        }
        
        public IEnumerator<ILex> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }
}
