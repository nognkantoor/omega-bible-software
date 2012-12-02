using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Omega.Model.Bibles.Structure
{
    public class Book
    {
        public enum BookType
        {
            NewTestament,
            OldTestament,
            Apokrypha
        }

        private static int _newTestament = 0;
        private static int _oldTestament = 0;
        private static List<Book> _books;

        public static List<Book> Books
        {
            get
            {
                if (_books == null)
                {
                    _books = OldTestament.Books
                        .Concat(NewTestament.Books).ToList();

                    Book lastBook = null;
                    foreach (Book b in _books)
                    {
                        b.SetPreviousBook(lastBook);
                        lastBook = b;
                    }

                }

                return _books;
            }
        }

        internal static Book Create(BookType testament, string name, string abbreviation)
        {
            if (testament == BookType.NewTestament) _newTestament++;
            else if (testament == BookType.OldTestament) _oldTestament++;
            Book b = new Book(testament == BookType.NewTestament ? _newTestament : _oldTestament,testament,name, abbreviation);
            return b;
        }

        internal Book(int bookNumber, BookType testament, string name, string abbreviation)
        {
            Number = bookNumber;
            Testament = testament;
            Name = name;
            Abbreviation = abbreviation;
        }

        private List<Chapter> _chapters = new List<Chapter>();
        private int _verses = -1;

        internal void SetPreviousBook(Book book)
        {
            Previous = book;
            if (book != null) book.SetNextBook(this);
        }

        internal void InitializeChapterChain()
        {
            Chapter last = Previous != null ? Previous.LastChapter : null;
            foreach (Chapter chapter in _chapters)
            {
                chapter.SetPreviousChapter(last);
                last = chapter;
            }
        }

        internal void SetNextBook(Book book)
        {
            Next = book;
            InitializeChapterChain();
            if (book != null)
            {
                book.FirstChapter.SetPreviousChapter(this.LastChapter);
            }
        }

        public int EndingVerse
        {
            get
            {
                return LastChapter != null ? LastChapter.EndingVerse : 1;
            }
        }

        public int StartingVerse
        {
            get
            {
                return FirstChapter != null ? FirstChapter.StartingVerse : 1;
            }
        }

        public Book Previous
        {
            get;
            protected set;
        }

        public Book Next
        {
            get;
            protected set;
        }

        public int Chapters
        {
            get
            {
                return _chapters.Count;
            }
        }

        public Chapter LastChapter
        {
            get
            {
                if (_chapters != null && _chapters.Count > 0)
                {
                    return _chapters[_chapters.Count - 1];
                }
                return null;
            }
        }

        public Chapter FirstChapter
        {
            get
            {
                if (_chapters != null && _chapters.Count > 0)
                {
                    return _chapters[0];
                }
                return null;
            }
        }

        public Chapter this[int chapter]
        {
            get
            {
                if(chapter > 0 && chapter <= _chapters.Count)
                {
                    return _chapters[chapter-1];
                }
                return null;
            }
        }

        internal Book AddChapters(params int [] versesInChapters)
        {
            int chapter = _chapters.Count+1;
            _verses = 0;
            for(int i=0; i<versesInChapters.Length; i++)
            {
                _verses += versesInChapters[i];
                AddChapter(chapter+i, versesInChapters[i]); 
            }
            return this;
        }
            
        internal Book AddChapter(int verses)
        {
            return AddChapter(_chapters.Count+1, verses);
        }
            
        internal Book AddChapter(int number, int verses)
        {
            _chapters.Add(new Chapter(this)
                .SetNumber(number)
                .SetVerses(verses));
            return this;

        }

        public int AllVerses
        {
            get
            {
                if (_verses < 0)
                {
                    _verses = _chapters.Sum(c => c.Verses);
                }
                return _verses;
            }
        }

        public string Name
        {
            get;
            protected set;
        }

        public string Abbreviation
        {
            get;
            protected set;
        }

        public BookType Testament
        {
            get;
            protected set;
        }

        public int Number
        {
            get;
            protected set;
        }

        public override string  ToString()
        {
 	            return string.Format("Book of {0} ({1}), {2} chapters", Name, Abbreviation, Chapters);
        }
    } 
}
