using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Common.Core.MVVM
{
    /// <summary>
    /// Binding class is able of retrieving values by giving a property path
    /// and giving the root object. 
    /// <example>
    /// var thirdObject = new {PropertyC = "Target value"};
    /// var secondObject = new {PropertyB = thirdObject };
    /// var rootObject = new {PropertyA = secondObject };
    /// Binding binding = new Binding("PropertyA.PropertyB.PropertyC", rootObject);
    /// string value = (string)binding.Value; // value == "Target value"
    /// </example>
    /// </summary>
    public class Binding
    {
        #region Private fields

        /// <summary>
        /// Holds the path for the binding.
        /// </summary>
        private string _path = ".";

        /// <summary>
        /// Holds the root object for the path binding.
        /// </summary>
        private object _dataContext;

        /// <summary>
        /// Holds the object being at the top of the property path.
        /// This is the last object from the path. 
        /// Using this speeds up evaluating values when only 
        /// a PropertyChanged event occured on the last object.
        /// </summary>
        private object _topPart;

        /// <summary>
        /// Holds the subsequent object to the _topPart.
        /// </summary>
        private object _topPartOwner;

        /// <summary>
        /// Holds the PropertyInfo for the top object.
        /// </summary>
        private PropertyInfo _topPartProperty;

        /// <summary>
        /// Holds all the parts of the binding path.
        /// </summary>
        private List<KeyValuePair<string, object>> _parts = new List<KeyValuePair<string, object>>();

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Binding class.
        /// </summary>
        public Binding()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Binding class with
        /// binding path.
        /// </summary>
        /// <param name="path">Path to the value that the binding should evaluate.</param>
        public Binding(string path)
            : this(path, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Binding class
        /// with binding path and root object being the data context
        /// for the binding.
        /// </summary>
        /// <param name="path">Path to the value that the binding should evaluate.</param>
        /// <param name="dataContext">Data context object beinig the root of the binding.</param>
        public Binding(string path, object dataContext)
        {
            Path = path;
            DataContext = dataContext;
        }
        
        #endregion Constructors

        #region Evaluation methods

        /// <summary>
        /// Evaluates the binding from scratch.
        /// </summary>
        protected void EvaluateBinding()
        {
            EvaluateBinding(DataContext, Path);
        }

        /// <summary>
        /// Evaluates the binding from the given place.
        /// </summary>
        /// <param name="dataContext">Object to start the evaluation from.</param>
        /// <param name="path">Path left to evaluate from the given dataContext object.</param>
        protected void EvaluateBinding(object dataContext, string path)
        {
            EvaluateBinding(dataContext, path, string.Empty);
        }

        /// <summary>
        /// Evaluates the binding from the given place.
        /// </summary>
        /// <param name="dataContext">Object to start the evaluation from.</param>
        /// <param name="path">Path left to evaluate from the given dataContext object.</param>
        /// <param name="beginingPath">Path already evaluated.</param>
        protected void EvaluateBinding(object dataContext, string path, string beginingPath)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = ".";
            }

            if (dataContext == null) return;


            string[] parts = path.Split(new string[] { "." },
                StringSplitOptions.RemoveEmptyEntries);
            AddPartNotify(dataContext);
            if (string.IsNullOrEmpty(beginingPath))
            {
                ResetNotifyPropertyChanged();
                _parts.Add(new KeyValuePair<string, object>(".", dataContext));
            }
            else
            {
                _parts.Add(new KeyValuePair<string, object>(beginingPath, dataContext));
            }

            object lastPart = dataContext;
            object lastOwner = dataContext;
            object nextPart = null;
            PropertyInfo lastProperty = null;
            string partKey = beginingPath;

            for (int i = 0; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(partKey))
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
                    _parts.Add(new KeyValuePair<string, object>(partKey, nextPart));
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

        /// <summary>
        /// Removes listening for INotifyPropertyChanged.PropertyChanged event
        /// from all binding path elements.
        /// </summary>
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

        /// <summary>
        /// Adds listening for INotifyPropertyChanged.PropertyChanged event
        /// to the given part.
        /// </summary>
        /// <param name="value">Part object.</param>
        protected void AddPartNotify(object value)
        {
            if (value != null && value is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)value).PropertyChanged += new PropertyChangedEventHandler(BindingPropertyChanged);
            }
        }

        /// <summary>
        /// Removes listening for INotifyPropertyChanged.PropertyChanged event
        /// from the given binding path element.
        /// </summary>
        /// <param name="value">Part object.</param>
        protected void RemovePartNotify(object value)
        {
            if (value != null && value is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)value).PropertyChanged -= new PropertyChangedEventHandler(BindingPropertyChanged);
            }
        }

        /// <summary>
        /// Evaluates one part of the binding path by trying to
        /// get the next property value in the binding path.
        /// </summary>
        /// <param name="value">Current object in the path.</param>
        /// <param name="path">Next property in the path.</param>
        /// <param name="nextValue">Retrieved next value.</param>
        /// <param name="property">PropertyInfo of the path property if exists.</param>
        /// <returns>True if evaluation is successfull</returns>
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

        /// <summary>
        /// Handles the INotifyPropertyChanged.PropertyChanged event for a part
        /// of the binding.
        /// </summary>
        /// <param name="sender">Object which property changed.</param>
        /// <param name="e">PropertyChangedEventArgs for the property changed.</param>
        private void BindingPropertyChanged(object sender, PropertyChangedEventArgs e)
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

                string partsToRight = Path.Substring(partFrom.Key.Length + 1);
                int index = _parts.IndexOf(partFrom);

                if (_parts.Count > 0)
                {
                    for (int i = index; i < _parts.Count; i++)
                    {
                        RemovePartNotify(_parts[i].Value);
                    }
                }

                _parts.RemoveRange(index, _parts.Count - index);
                EvaluateBinding(partFrom.Value, partsToRight, partFrom.Key);
            }
        }


        #endregion Evaluation methods

        #region Public properties

        /// <summary>
        /// Gets or tries to set the value evaluated by the binding.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the data context object
        /// being the root of the binding.
        /// </summary>
        public object DataContext
        {
            get { return _dataContext; }
            set
            {
                _dataContext = value;
                EvaluateBinding();
            }
        }

        /// <summary>
        /// Gets or sets a dot separated path to the
        /// target value of the binding.
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                EvaluateBinding();
            }
        }

        #endregion Public properties
    }
}
