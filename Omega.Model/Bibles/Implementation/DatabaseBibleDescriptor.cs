using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Model.Bibles.Implementation
{
    public class DatabaseBibleDescriptor : IBibleDescriptor
    {
        public const string BIBLE_EXTENSIOSN = ".bblx";

        public DatabaseBibleDescriptor(string id, string connectionString)
        {
            BibleId = id;
            BibleSource = connectionString;
        }

        public string BibleId
        {
            get;
            protected set;
        }

        public string BibleSource
        {
            get;
            protected set;
        }
    }
}
