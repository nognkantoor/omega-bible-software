using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.Bibles.Structure
{
    public class Chapter
    {
        private int startingVerse = 1;
        private int endingVerse = 1;

        internal Chapter(Book parent)
            : this(parent, 0)
        {
        }

        protected Chapter(Book parent, int number)
        {
            Book = parent;
            Number = number;
        }

        internal Chapter SetNumber(int number)
        {
            Number = number;
            return this;
        }

        internal Chapter SetVerses(int verses)
        {
            Verses = verses;
            return this;
        }

        public Book Book
        {
            get;
            protected set;
        }

        internal void SetPreviousChapter(Chapter chapter)
        {
            Previous = chapter;
            if (chapter != null) chapter.Next = this;
        }

        public Chapter Previous
        {
            get;
            protected set;
        }

        public Chapter Next
        {
            get;
            protected set;
        }

        public int StartingVerse
        {
            get
            {
                if(Previous != null)
                {
                    startingVerse = Previous.EndingVerse + 1;
                }

                return startingVerse;
            }
        }

        public int EndingVerse
        {
            get
            {
                return StartingVerse + Verses - 1;
            }
        }

        public int Number
        {
            get;
            protected set;
        }

        public int Verses
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return string.Format("{0} - Chapter {1}, {2} verses", Book.Name, Number, Verses);
        }
    }
}
