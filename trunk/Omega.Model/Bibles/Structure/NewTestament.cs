using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Omega.Model.Bibles.Structure
{
    public static class NewTestament
    {

        static NewTestament()
        {
            List<Book> booksList = new List<Book>();
            booksList.Add(NewTestament.Mathew);
            booksList.Add(Mark);
            booksList.Add(Luke);
            booksList.Add(John);
            booksList.Add(Acts);
            booksList.Add(Romans);
            booksList.Add(Corinthians1);
            booksList.Add(Corinthians2);
            booksList.Add(Galatians);
            booksList.Add(Ephesians); 
            booksList.Add(Philippians);
            booksList.Add(Colossians);
            booksList.Add(Thessalonians1);
            booksList.Add(Thessalonians2);
            booksList.Add(Timoth1);
            booksList.Add(Timothy2);
            booksList.Add(Titus);
            booksList.Add(Philemon);
            booksList.Add(Hebrews);
            booksList.Add(James);
            booksList.Add(Peter1);
            booksList.Add(Peter2);
            booksList.Add(John1);
            booksList.Add(John2);
            booksList.Add(John3);
            booksList.Add(Jude);
            booksList.Add(Revelation);
            NewTestament.Books = booksList.AsReadOnly();
        }

        public static ReadOnlyCollection<Book> Books
        {
            get;
            private set;
        }

        public static readonly Book Mathew = Book.Create(Book.BookType.NewTestament,"Mathew", "Mt").AddChapters(25, 23, 17, 25, 48, 34, 29, 34, 38, 42, 30, 50, 58, 36, 39, 28, 27, 35, 30, 34, 46, 46, 39, 51, 46, 75, 66, 20);
        public static readonly Book Mark = Book.Create(Book.BookType.NewTestament,"Mark", "Mk").AddChapters(45, 28, 35, 41, 43, 56, 37, 38, 50, 52, 33, 44, 37, 72, 47, 20);
        public static readonly Book Luke = Book.Create(Book.BookType.NewTestament,"Luke", "Lk").AddChapters(80, 52, 38, 44, 39, 49, 50, 56, 62, 42, 54, 59, 35, 35, 32, 31, 37, 43, 48, 47, 38, 71, 56, 53);
        public static readonly Book John = Book.Create(Book.BookType.NewTestament,"John", "Jn").AddChapters(51, 25, 36, 54, 47, 71, 53, 59, 41, 42, 57, 50, 38, 31, 27, 33, 26, 40, 42, 31, 25);
        public static readonly Book Acts = Book.Create(Book.BookType.NewTestament,"Acts", "Acts").AddChapters(26, 47, 26, 37, 42, 15, 60, 40, 43, 48, 30, 25, 52, 28, 41, 40, 34, 28, 40, 38, 40, 30, 35, 27, 27, 32, 44, 31);
        public static readonly Book Romans = Book.Create(Book.BookType.NewTestament,"Romans", "Rom").AddChapters(32, 29, 31, 25, 21, 23, 25, 39, 33, 21, 36, 21, 14, 23, 33, 27);
        public static readonly Book Corinthians1 = Book.Create(Book.BookType.NewTestament,"1 Corinthians", "1Cor").AddChapters(31, 16, 23, 21, 13, 20, 40, 13, 27, 33, 34, 31, 13, 40, 58, 24);
        public static readonly Book Corinthians2 = Book.Create(Book.BookType.NewTestament,"2 Corinthians", "2Cor").AddChapters(24, 17, 18, 18, 21, 18, 16, 24, 15, 18, 33, 21, 13);
        public static readonly Book Galatians = Book.Create(Book.BookType.NewTestament,"Galatians", "Gal").AddChapters(24, 21, 29, 31, 26, 18);
        public static readonly Book Ephesians = Book.Create(Book.BookType.NewTestament,"Ephesians", "Eph").AddChapters(23, 22, 21, 32, 33, 24);
        public static readonly Book Philippians = Book.Create(Book.BookType.NewTestament,"Philippians", "Phil").AddChapters(30, 30, 21, 23);
        public static readonly Book Colossians = Book.Create(Book.BookType.NewTestament,"Colossians", "Col").AddChapters(29, 23, 25, 18);
        public static readonly Book Thessalonians1 = Book.Create(Book.BookType.NewTestament,"1 Thessalonians", "1Thess").AddChapters(10, 20, 13, 18, 28);
        public static readonly Book Thessalonians2 = Book.Create(Book.BookType.NewTestament,"2 Thessalonians", "2Thess").AddChapters(12, 17, 18);
        public static readonly Book Timoth1 = Book.Create(Book.BookType.NewTestament,"1 Timoth", "1Tim").AddChapters(20, 15, 16, 16, 25, 21);
        public static readonly Book Timothy2 = Book.Create(Book.BookType.NewTestament,"2 Timothy", "2Tim").AddChapters(18, 26, 17, 22);
        public static readonly Book Titus = Book.Create(Book.BookType.NewTestament,"Titus", "Titus").AddChapters(16, 15, 15);
        public static readonly Book Philemon = Book.Create(Book.BookType.NewTestament,"Philemon", "Philemon").AddChapters(25);
        public static readonly Book Hebrews = Book.Create(Book.BookType.NewTestament,"Hebrews", "Heb").AddChapters(14, 18, 19, 16, 14, 20, 28, 13, 28, 39, 40, 29, 25);
        public static readonly Book James = Book.Create(Book.BookType.NewTestament,"James", "Jas").AddChapters(27, 26, 18, 17, 20);
        public static readonly Book Peter1 = Book.Create(Book.BookType.NewTestament,"1 Peter", "1Pet").AddChapters(25, 25, 22, 19, 14);
        public static readonly Book Peter2 = Book.Create(Book.BookType.NewTestament,"2 Peter", "2Pet").AddChapters(21, 22, 18);
        public static readonly Book John1 = Book.Create(Book.BookType.NewTestament,"1 John", "1Jn").AddChapters(10, 29, 24, 21, 21);
        public static readonly Book John2 = Book.Create(Book.BookType.NewTestament,"2 John", "2Jn").AddChapters(13);
        public static readonly Book John3 = Book.Create(Book.BookType.NewTestament,"3 John", "3Jn").AddChapters(15);
        public static readonly Book Jude = Book.Create(Book.BookType.NewTestament,"Jude", "Jude").AddChapters(25);
        public static readonly Book Revelation = Book.Create(Book.BookType.NewTestament,"Revelation", "Rev").AddChapters(20, 29, 22, 11, 14, 17, 17, 13, 21, 11, 19, 17, 18, 20, 8, 21, 18, 24, 21, 15, 27, 21);

    }
}
