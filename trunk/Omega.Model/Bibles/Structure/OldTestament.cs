using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Omega.Model.Bibles.Structure
{
    
    public class OldTestament
    {
        static OldTestament()
        {
            List<Book> books = new List<Book>();
            books.Add(Genesis);
            books.Add(Exodus);
            books.Add(Leviticus );
            books.Add(Numbers);
            books.Add(Deuteronomy);
            books.Add(Joshua);
            books.Add(Judges);
            books.Add(Ruth);
            books.Add(Samuel1);
            books.Add(Samuel2);
            books.Add(Kings1);
            books.Add(Kings2);
            books.Add(Chronicles1);
            books.Add(Chronicles2);
            books.Add(Ezra);
            books.Add(Nehemiah);
            books.Add(Esther);
            books.Add(Job);
            books.Add(Psalms);
            books.Add(Proverbs);
            books.Add(Ecclesiastes);
            books.Add(SongofSolomon);
            books.Add(Isaiah);
            books.Add(Jeremiah);
            books.Add(Lamentations);
            books.Add(Ezekiel);
            books.Add(Daniel);
            books.Add(Hosea);
            books.Add(Joel);
            books.Add(Amos);
            books.Add(Obadiah);
            books.Add(Jonah);
            books.Add(Micah);
            books.Add(Nahum);
            books.Add(Habakkuk);
            books.Add(Zephaniah); 
            books.Add(Haggai);
            books.Add(Zechariah);
            books.Add(Malachi);
            OldTestament.Books = books.AsReadOnly();
        }

        public static ReadOnlyCollection<Book> Books
        {
            get;
            private set;
        }

        public static readonly Book Genesis = Book.Create(Book.BookType.OldTestament,"Genesis", "Gen").AddChapters(31, 25, 24, 26, 32, 22, 24, 22, 29, 32, 32, 20, 18, 24, 21, 16, 27, 33, 38, 18, 34, 24, 20, 67, 34, 35, 46, 22, 35, 43, 54, 33, 20, 31, 29, 43, 36, 30, 23, 23, 57, 38, 34, 34, 28, 34, 31, 22, 33, 26);
        public static readonly Book Exodus = Book.Create(Book.BookType.OldTestament,"Exodus", "Ex").AddChapters(22, 25, 22, 31, 23, 30, 29, 28, 35, 29, 10, 51, 22, 31, 27, 36, 16, 27, 25, 26, 37, 30, 33, 18, 40, 37, 21, 43, 46, 38, 18, 35, 23, 35, 35, 38, 29, 31, 43, 38);
        public static readonly Book Leviticus = Book.Create(Book.BookType.OldTestament,"Leviticus", "Lev").AddChapters(17, 16, 17, 35, 26, 23, 38, 36, 24, 20, 47, 8, 59, 57, 33, 34, 16, 30, 37, 27, 24, 33, 44, 23, 55, 46, 34);
        public static readonly Book Numbers = Book.Create(Book.BookType.OldTestament, "Numbers", "Num").AddChapters(54, 34, 51, 49, 31, 27, 89, 26, 23, 36, 35, 16, 33, 45, 41, 50, 13, 32, 22, 29, 35, 41, 30, 25, 18, 65, 23, 31, 40, 16, 54, 42, 56, 29, 34, 13);
        public static readonly Book Deuteronomy = Book.Create(Book.BookType.OldTestament,"Deuteronomy", "Deut").AddChapters(46, 37, 29, 49, 33, 25, 26, 20, 29, 22, 32, 31, 19, 29, 23, 22, 20, 22, 21, 20, 23, 29, 26, 22, 19, 19, 26, 69, 28, 20, 30, 52, 29, 12);
        public static readonly Book Joshua = Book.Create(Book.BookType.OldTestament,"Joshua", "Josh").AddChapters(18, 24, 17, 24, 15, 27, 26, 35, 27, 43, 23, 24, 33, 15, 63, 10, 18, 28, 51, 9, 45, 34, 16, 33);
        public static readonly Book Judges = Book.Create(Book.BookType.OldTestament,"Judges", "Judg").AddChapters(36, 23, 31, 24, 31, 40, 25, 35, 57, 18, 40, 15, 25, 20, 20, 31, 13, 31, 30, 48, 25);
        public static readonly Book Ruth = Book.Create(Book.BookType.OldTestament,"Ruth", "Ruth").AddChapters(22, 23, 18, 22);
        public static readonly Book Samuel1 = Book.Create(Book.BookType.OldTestament,"1 Samuel", "1Sam").AddChapters(28, 36, 21, 22, 12, 21, 17, 22, 27, 27, 15, 25, 23, 52, 35, 23, 58, 30, 24, 42, 16, 23, 28, 23, 43, 25, 12, 25, 11, 31, 13);
        public static readonly Book Samuel2 = Book.Create(Book.BookType.OldTestament,"2 Samuel", "2Sam").AddChapters(27, 32, 39, 12, 25, 23, 29, 18, 13, 19, 27, 31, 39, 33, 37, 23, 29, 32, 44, 26, 22, 51, 39, 25);
        public static readonly Book Kings1 = Book.Create(Book.BookType.OldTestament,"1 Kings", "1Kings").AddChapters(53, 46, 28, 20, 32, 38, 51, 66, 28, 29, 43, 33, 34, 31, 34, 34, 24, 46, 21, 43, 29, 54);
        public static readonly Book Kings2 = Book.Create(Book.BookType.OldTestament,"2 Kings", "2Kings").AddChapters(18, 25, 27, 44, 27, 33, 20, 29, 37, 36, 20, 22, 25, 29, 38, 20, 41, 37, 37, 21, 26, 20, 37, 20, 30);
        public static readonly Book Chronicles1 = Book.Create(Book.BookType.OldTestament,"1 Chronicles", "1Chr").AddChapters(54, 55, 24, 43, 41, 66, 40, 40, 44, 14, 47, 41, 14, 17, 29, 43, 27, 17, 19, 8, 30, 19, 32, 31, 31, 32, 34, 21, 30);
        public static readonly Book Chronicles2 = Book.Create(Book.BookType.OldTestament,"2 Chronicles", "2Chr").AddChapters(18, 17, 17, 22, 14, 42, 22, 18, 31, 19, 23, 16, 23, 14, 19, 14, 19, 34, 11, 37, 20, 12, 21, 27, 28, 23, 9, 27, 36, 27, 21, 33, 25, 33, 26, 23);
        public static readonly Book Ezra = Book.Create(Book.BookType.OldTestament,"Ezra", "Ezra").AddChapters(11, 70, 13, 24, 17, 22, 28, 36, 15, 44);
        public static readonly Book Nehemiah = Book.Create(Book.BookType.OldTestament,"Nehemiah", "Neh").AddChapters(11, 20, 38, 17, 19, 19, 72, 18, 37, 40, 36, 47, 31);
        public static readonly Book Esther = Book.Create(Book.BookType.OldTestament,"Esther", "Esth").AddChapters(22, 23, 15, 17, 14, 14, 10, 17, 32, 3, 17, 8, 30, 16, 24, 10);
        public static readonly Book Job = Book.Create(Book.BookType.OldTestament,"Job", "Job").AddChapters(22, 13, 26, 21, 27, 30, 21, 22, 35, 22, 20, 25, 28, 22, 35, 22, 16, 21, 29, 29, 34, 30, 17, 25, 6, 14, 21, 28, 25, 31, 40, 22, 33, 37, 16, 33, 24, 41, 30, 32, 26, 17);
        public static readonly Book Psalms = Book.Create(Book.BookType.OldTestament,"Psalms", "Ps").AddChapters(6, 11, 9, 9, 13, 11, 18, 10, 21, 18, 7, 9, 6, 7, 5, 11, 15, 51, 15, 10, 14, 32, 6, 10, 22, 11, 14, 9, 11, 13, 25, 11, 22, 23, 28, 13, 40, 23, 14, 18, 14, 12, 5, 27, 18, 12, 10, 15, 21, 23, 21, 11, 7, 9, 24, 14, 12, 12, 18, 14, 9, 13, 12, 11, 14, 20, 8, 36, 37, 6, 24, 20, 28, 23, 11, 13, 21, 72, 13, 20, 17, 8, 19, 13, 14, 17, 7, 19, 53, 17, 16, 16, 5, 23, 11, 13, 12, 9, 9, 5, 8, 29, 22, 35, 45, 48, 43, 14, 31, 7, 10, 10, 9, 8, 18, 19, 2, 29, 176, 7, 8, 9, 4, 8, 5, 6, 5, 6, 8, 8, 3, 18, 3, 3, 21, 26, 9, 8, 24, 14, 10, 8, 12, 15, 21, 10, 20, 14, 9, 6);
        public static readonly Book Proverbs = Book.Create(Book.BookType.OldTestament,"Proverbs", "Prov").AddChapters(33, 22, 35, 27, 23, 35, 27, 36, 18, 32, 31, 28, 25, 35, 33, 33, 28, 24, 29, 30, 31, 29, 35, 34, 28, 28, 27, 28, 27, 33, 31);
        public static readonly Book Ecclesiastes = Book.Create(Book.BookType.OldTestament,"Ecclesiastes", "Ecc").AddChapters(18, 26, 22, 17, 19, 12, 29, 17, 18, 20, 10, 14);
        public static readonly Book SongofSolomon = Book.Create(Book.BookType.OldTestament,"Song of Solomon", "Song").AddChapters(17, 17, 11, 16, 16, 12, 14, 14);
        public static readonly Book Isaiah = Book.Create(Book.BookType.OldTestament,"Isaiah", "Isa").AddChapters(31, 22, 26, 6, 30, 13, 25, 23, 20, 34, 16, 6, 22, 32, 9, 14, 14, 7, 25, 6, 17, 25, 18, 23, 12, 21, 13, 29, 24, 33, 9, 20, 24, 17, 10, 22, 38, 22, 8, 31, 29, 25, 28, 28, 25, 13, 15, 22, 26, 11, 23, 15, 12, 17, 13, 12, 21, 14, 21, 22, 11, 12, 19, 11, 25, 24);
        public static readonly Book Jeremiah = Book.Create(Book.BookType.OldTestament,"Jeremiah", "Jer").AddChapters(19, 37, 25, 31, 31, 30, 34, 23, 25, 25, 23, 17, 27, 22, 21, 21, 27, 23, 15, 18, 14, 30, 40, 10, 38, 24, 22, 17, 32, 24, 40, 44, 26, 22, 19, 32, 21, 28, 18, 16, 18, 22, 13, 30, 5, 28, 7, 47, 39, 46, 64, 34);
        public static readonly Book Lamentations = Book.Create(Book.BookType.OldTestament,"Lamentations", "Lam").AddChapters(22, 22, 66, 22, 22);
        public static readonly Book Ezekiel = Book.Create(Book.BookType.OldTestament,"Ezekiel", "Ezek").AddChapters(28, 10, 27, 17, 17, 14, 27, 18, 11, 22, 25, 28, 23, 23, 8, 63, 24, 32, 14, 44, 37, 31, 49, 27, 17, 21, 36, 26, 21, 26, 18, 32, 33, 31, 15, 38, 28, 23, 29, 49, 26, 20, 27, 31, 25, 24, 23, 35);
        public static readonly Book Daniel = Book.Create(Book.BookType.OldTestament,"Daniel", "Dan").AddChapters(21, 49, 100, 34, 30, 29, 28, 27, 27, 21, 45, 13, 64, 42);
        public static readonly Book Hosea = Book.Create(Book.BookType.OldTestament,"Hosea", "Hos").AddChapters(9, 25, 5, 19, 15, 11, 16, 14, 17, 15, 11, 15, 15, 10);
        public static readonly Book Joel = Book.Create(Book.BookType.OldTestament,"Joel", "Joel").AddChapters(20, 27, 5, 21);
        public static readonly Book Amos = Book.Create(Book.BookType.OldTestament,"Amos", "Am").AddChapters(15, 16, 15, 13, 27, 14, 17, 14, 15);
        public static readonly Book Obadiah = Book.Create(Book.BookType.OldTestament,"Obadiah", "Ob").AddChapters(21);
        public static readonly Book Jonah = Book.Create(Book.BookType.OldTestament,"Jonah", "Jon").AddChapters(16, 11, 10, 11);
        public static readonly Book Micah = Book.Create(Book.BookType.OldTestament,"Micah", "Mic").AddChapters(16, 13, 12, 14, 14, 16, 20);
        public static readonly Book Nahum = Book.Create(Book.BookType.OldTestament,"Nahum", "Nah").AddChapters(14, 14, 19);
        public static readonly Book Habakkuk = Book.Create(Book.BookType.OldTestament,"Habakkuk", "Hab").AddChapters(17, 20, 19);
        public static readonly Book Zephaniah = Book.Create(Book.BookType.OldTestament,"Zephaniah", "Zeph").AddChapters(18, 15, 20);
        public static readonly Book Haggai = Book.Create(Book.BookType.OldTestament,"Haggai", "Hag").AddChapters(15, 23);
        public static readonly Book Zechariah = Book.Create(Book.BookType.OldTestament,"Zechariah", "Zech").AddChapters(17, 17, 10, 14, 11, 15, 14, 23, 17, 12, 17, 14, 9, 21);
        public static readonly Book Malachi = Book.Create(Book.BookType.OldTestament,"Malachi", "Mal").AddChapters(14, 17, 24);
    }
}
