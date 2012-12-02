using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.MVVM;
using System.Xml.Linq;

namespace Omega.Tools.BibleParser
{
    public class HtmlAttribute : ViewModelBase, IComparable<XAttribute>
    {
        private string _name;
        private string _value;

        public HtmlAttribute()
        {
        }

        public HtmlAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }


        public int CompareTo(XAttribute other)
        {
            int result = string.Compare(Name, other.Name.LocalName, true);
            result += string.Compare(Value, other.Value, true);
            return result;
        }

    }

}