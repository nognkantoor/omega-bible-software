using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.Format;
using System.IO;

namespace Omega.Model.Bibles.Implementation
{
    public class FileBibleDescriptor : IBibleDescriptor
    {
        public const string BIBLE_EXTENSION = ".bbl";

        public FileBibleDescriptor(string folder, string bibleName)
        {
            BibleId = bibleName.MakeSlug();
            BibleSource = Path.Combine(folder, bibleName + BIBLE_EXTENSION);
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
