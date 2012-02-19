using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Common.Core.MVVM
{
    public class Binding
    {
        private string _path = ".";
        private object _dataContext;
        private object _topPart;
        private object _topPartOwner;
        private PropertyInfo _topPartProperty;
        private List<KeyValuePair<string, object>> _parts = new List<KeyValuePair<string, object>>();

        public Binding()
        {
 
        }

        public Binding(string path)
            : this(path,null)
        {
        }

        public Binding(string path, object dataContext)
        {
            Path = path;
            DataContext = dataContext;
        }

        protected void EvaluateBinding()
        {
            EvaluateBinding(DataContext, Path);
        }

        protected void EvaluateBinding(object dataContext, string path)
        {
            EvaluateBinding(dataContext, path, string.Empty);
        }

        protected void EvaluateBinding(object dataContext, string path, string beginingPath)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = ".";
            }

            if(dataContext == null) return;
            

            string[] parts = path.Split(new string [] { "." }, 
                StringSplitOptions.RemoveEmptyEntries);
            AddPartNotify(dataContext);
            if (string.IsNullOrEmpty(beginingPath))
            {
                ResetNotifyPropertyChanged();
                _parts.Add(new KeyValuePair<string, object>(".", dataContext));
            }
            else
            {
                _parts.Add(new KeyValuePair<string,object>(beginingPath, dataContext));
            }

            object lastPart = dataContext;
            object lastOwner = dataContext;
            object nextPart = null;
            PropertyInfo lastProperty = null;
            string partKey = beginingPath;

            for (int i = 0; i < parts.Length; i++)
            {
                if(string.IsNullOrEmpty(partKey))
                {
                    partKey = parts[i];
                }
                else
                {
                    partKey += "." + parts[i];
                }

                if (EvaluatePart(lastPart, parts[i], out nextPart, out lastProperty))
                {
                    lastOwner = lastPart;
                    _parts.Add(new KeyValuePair<string,object>(partKey, nextPart));
                    lastPart = nextPart;
                    nextPart = null;
                }
                else
                {
                    // Binding error
                    return;
                }
            }

            _topPartOwner = lastOwner;
            _topPart = lastPart;
            _topPartProperty = lastProperty;
        }

        private void ResetNotifyPropertyChanged()
        {
            if (_parts.Count > 0)
            {
                foreach (KeyValuePair<string, object> p in _parts)
                {
                    RemovePartNotify(p.Value);
                }
            }
        }

        protected void AddPartNotify(object value)
        {
            if (value != null && value is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)value).PropertyChanged += new PropertyChangedEventHandler(BindingPropertyChanged);
            }
        }

        protected void RemovePartNotify(object value)
        {
            if (value != null && value is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)value).PropertyChanged -= new PropertyChangedEventHandler(BindingPropertyChanged);
            }
        }

        protected bool EvaluatePart(object value, string path, out object nextValue, out PropertyInfo property)
        {
            property = null;
            if (path != null && value != null)
            {
                property = value.GetType().GetProperty(path);
                if (property != null)
                {
                    nextValue = property.GetValue(value, null);
                    AddPartNotify(nextValue);
                    return true;
                }
            }

            nextValue = null;
            return false;
        }

        void BindingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            KeyValuePair<string, object> partFrom = _parts.FirstOrDefault(p => p.Value == sender);
            if (partFrom.Key != null && partFrom.Value != null)
            {
                if (partFrom.Key == ".")
                {
                    // Do everything from the begining.
                    EvaluateBinding();
                    return;
                }

                string partsToRight = Path.Substring(partFrom.Key.Length+1);
                int index = _parts.IndexOf(partFrom);

                if (_parts.Count > 0)
                {
                    for(int i=index;i < _parts.Count;i++)
                    {
                        RemovePartNotify(_parts[i].Value);
                    }
                }

                _parts.RemoveRange(index, _parts.Count - index);
                EvaluateBinding(partFrom.Value, partsToRight, partFrom.Key);
            }
        }

        public object Value
        {
            get { return _topPart; }
            set 
            {
                if (!string.IsNullOrEmpty(Path) && 
                    _topPartOwner != null && 
                    _topPartProperty != null &&
                    _topPartProperty.CanWrite)
                {
                    _topPartProperty.SetValue(_topPartOwner, value, null);
                }
            }
        }

        public object DataContext
        {
            get { return _dataContext; }
            set
            {
                _dataContext = value;
                EvaluateBinding();
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                EvaluateBinding();
            }
        }

    }
}
