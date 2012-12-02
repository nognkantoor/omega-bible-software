using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.Collections;
using Omega.Model.Bibles;

namespace Omega.Model.BibleText.Implementation
{
    public abstract class BaseBibleProvider : IBibleProvider
    {
        public BaseBibleProvider()
        {
            VerseCache = new Dictionary<string, IDictionary<int, IList<IVerse>>>();
        }

        protected IDictionary<string, IDictionary<int, IList<IVerse>>> VerseCache
        {
            get;
            private set;
        }

        protected abstract IList<IVerse> GetVersesOverride(string book, int chapter);
        public abstract void Initialize(IBibleDescriptor descriptor);

        public IList<IVerse> GetVerses(string book, int chapter)
        {
            IDictionary<int, IList<IVerse>> bookChapters = VerseCache.GetForKey(book);
            IList<IVerse> verses = bookChapters.GetForKey(chapter);
            if (bookChapters == null)
            {
                VerseCache[book] = new Dictionary<int, IList<IVerse>>();
                VerseCache[book][chapter] = verses = GetVersesOverride(book, chapter);
            }
            if (verses == null)
            {
                VerseCache[book][chapter] = verses = GetVersesOverride(book, chapter);
            }
            return verses;
        }

        public IList<IVerse> GetVerses(string book, int chapter, int fromVerse, int toVerse)
        {
            IList<IVerse> verses = GetVerses(book, chapter);
            return verses.TakePart(fromVerse - 1, toVerse - 1);
        }
    }
}
