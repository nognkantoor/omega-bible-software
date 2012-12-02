using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Tools.BibleParser
{
    public class VerseInformation
    {
        public enum WordType
        {
            Hebrew,
            Greek
        }

        public class Word
        {
            public Word(string word, WordType type, int strong)
            {
                Value = word;
                Type = type;
                Strong = strong;
            }

            public string Value
            {
                get;
                protected set;
            }

            public WordType Type
            {
                get;
                protected set;
            }

            public int Strong
            {
                get;
                protected set;
            }
        }

        public VerseInformation()
        {
            Words = new List<Word>();   
        }

        public List<Word> Words
        {
            get;
            protected set;
        }

        public void AddWord(string word, WordType type, int strong)
        {
            Words.Add(new Word(word, type, strong));
        }
    }
}
