using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Model.Bibles.Implementation;
using System.IO;

namespace Omega.Model.BibleText.Implementation
{
    public class FileBibleProvider : BaseBibleProvider
    {
        FileBibleDescriptor _descriptor;
        string[] _lines;
        protected override IList<IVerse> GetVersesOverride(string book, int chapter)
        {
            if (_lines == null && _descriptor != null && File.Exists(_descriptor.BibleSource))
            {
                _lines = File.ReadAllLines(_descriptor.BibleSource);
            }
            List<IVerse> result = new List<IVerse>();
            if (_lines == null || _lines.Length == 0) return result;

            return null;
        }

        public override void Initialize(Bibles.IBibleDescriptor descriptor)
        {
            _descriptor = descriptor as FileBibleDescriptor;
        }
    }
}
