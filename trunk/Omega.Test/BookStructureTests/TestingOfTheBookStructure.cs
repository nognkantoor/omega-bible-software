using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omega.Model.Bibles.Structure;

namespace Omega.Test.BookStructureTests
{
    [TestFixture]
    public class TestingOfTheBookStructure
    {
        private Dictionary<Book, int> BookEndings
        {
            get
            {
                Dictionary<Book, int> endings = new Dictionary<Book, int>();
                endings.Add(OldTestament.Deuteronomy, 5852);
                endings.Add(OldTestament.Ruth, 7213);
                endings.Add(OldTestament.Chronicles2, 12017);
                endings.Add(OldTestament.Psalms, 16401);
                endings.Add(OldTestament.Daniel, 22095);
                return endings;
            }
        }

        [Test]
        public void TestBasic()
        {
            var books = Book.Books;
            Assert.AreNotEqual(0, books.Count);
            int lastVersePoint = -1;
            int sum = 0;

            var endings = BookEndings;

            foreach (Book b in OldTestament.Books)
            {
                if (b.Previous != null && endings.ContainsKey(b.Previous))
                {
                    Assert.AreEqual(endings[b.Previous], lastVersePoint);
                }
                Assert.Greater(b.StartingVerse, lastVersePoint);
                Assert.Greater(b.EndingVerse, b.StartingVerse);
                lastVersePoint = b.EndingVerse;

                for (int i = 0; i < b.Chapters; i++)
                {
                    sum += b[i+1].Verses;
                }

                Assert.AreEqual(sum, lastVersePoint);
            }

            // This is the end of the old testament, according to TheWord bible files.
            Assert.AreEqual(23145, lastVersePoint);

            // This is the end of the old testament, according to TheWord bible files.
            foreach (Book b in NewTestament.Books)
            {
                if (endings.ContainsKey(b.Previous))
                {
                    Assert.AreEqual(endings[b.Previous], lastVersePoint);
                }

                Assert.Greater(b.StartingVerse, lastVersePoint);
                Assert.Greater(b.EndingVerse, b.StartingVerse);
                lastVersePoint = b.EndingVerse;

                for (int i = 0; i < b.Chapters; i++)
                {
                    sum += b[i + 1].Verses;
                }

                Assert.AreEqual(sum, lastVersePoint);
            }
            Assert.AreEqual(31102, lastVersePoint);
        }
    }
}
