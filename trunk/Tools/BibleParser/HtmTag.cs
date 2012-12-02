using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.MVVM;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Omega.Tools.BibleParser
{
    public class HtmlTag : ViewModelBase, IComparable<XElement>
    {
        private string _tag;
        private ObservableCollection<HtmlAttribute> _attributes = new ObservableCollection<HtmlAttribute>();

        public string Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public ObservableCollection<HtmlAttribute> Attributes
        {
            get
            {
                return _attributes;
            }

            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        public int CompareTo(XElement other)
        {
            int result = string.Compare(Tag, other.Name.LocalName, true);
            if (result != 0)
            {
                return result;
            }

            List<XAttribute> attributes = other.Attributes().ToList();
            Attributes.All<HtmlAttribute>(html =>
            {
                int diff = 0;
                var attr = attributes.FirstOrDefault(a => (diff = html.CompareTo(a)) == 0);
                if (attr == null)
                {
                    result--;
                }
                else
                {
                    result += diff;
                }
                return true;
            });

            return result;
        }
    }
}
