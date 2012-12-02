using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.MVVM;

namespace Common.Core.Collections
{
    /// <summary>
    /// FunctionDictionary implements the IDictionary interface
    /// but the getter and setter methods are provided by a function
    /// given in constructor for getting and setting a value
    /// by using a IDictionary interface.
    /// </summary>
    /// <typeparam name="TKey">Parameter type being the key for the dictionary</typeparam>
    /// <typeparam name="TResult">Parameter type being the result value of the dictionary</typeparam>
    public class FunctionDictionary<TKey,TResult> : 
        ViewModelBase, 
        Common.Interfaces.Core.Collections.INotifyDictionary<TKey,TResult>,
        IDictionary<TKey,TResult>
    {
        #region Private fields

        /// <summary>
        /// Holds the getter function for the dictionary
        /// having one input parameter TKey and returning the value 
        /// corelated with the given key.
        /// </summary>
        private Func<TKey, TResult> _getterFunction;

        /// <summary>
        /// Holds the setter action for the dictionary
        /// having one input parameter TKey and TResult as the value
        /// to be set for the given key.
        /// </summary>
        private Action<TKey, TResult> _setterAction;

        /// <summary>
        /// Holds the getter function for default TResult
        /// having one input parameter TKey and returning the value 
        /// corelated with the given key.
        /// </summary>
        private Func<TKey, TResult> _defaultValueGetter;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FunctionDictionary class with a getter function.
        /// This kind of FunctionDictionary will be able only to get values.
        /// </summary>
        /// <param name="getterFunction">Function for getting a value by key.</param>
        public FunctionDictionary(Func<TKey, TResult> getterFunction)
            : this(getterFunction, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the FunctionDictionary class with a getter function
        /// and a default value getter.
        /// This kind of FunctionDictionary will be able only to get values.
        /// This dictionary will return value from the defaultValueGetter function when the normal 
        /// getter will return null.
        /// </summary>
        /// <param name="getterFunction">Function for getting a value by key.</param>
        /// <param name="defaultValueGetter">Function for getting a default TResult value.</param>
        public FunctionDictionary(Func<TKey, TResult> getterFunction,
            Func<TKey, TResult> defaultValueGetter)
            : this(getterFunction, defaultValueGetter, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FunctionDictionary class with a getter function
        /// and setter action.
        /// </summary>
        /// <param name="getterFunction">Function for getting a value by key.</param>
        /// <param name="setterAction">Action setting a value for the given key.</param>
        public FunctionDictionary(Func<TKey, TResult> getterFunction,
            Action<TKey, TResult> setterAction)
            : this(getterFunction, null, setterAction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FunctionDictionary class with a getter function
        /// default value getter function, and setter action.
        /// This dictionary will return value from the defaultValueGetter function when the normal 
        /// getter will return null.
        /// </summary>
        /// <param name="getterFunction">Function for getting a value by key.</param>
        /// <param name="setterAction">Action setting a value for the given key.</param>
        /// <param name="defaultValueGetter">Function for getting a default TResult value.</param>
        public FunctionDictionary(Func<TKey, TResult> getterFunction,
            Func<TKey, TResult> defaultValueGetter,
            Action<TKey, TResult> setterAction)
        {
            _setterAction = setterAction;
            _getterFunction = getterFunction;
            _defaultValueGetter = defaultValueGetter;
        }

        #endregion Constructors

        #region IFunctionDictionary members

        /// <summary>
        /// Gets or sets the value depending on the given key.
        /// This property uses the getter function and setter action
        /// provided in the constructor.
        /// </summary>
        /// <param name="index">Key index</param>
        /// <returns>Result for the given key.</returns>
        public TResult this[TKey index]
        {
            get
            {
                TResult result = _defaultValueGetter != null ?
                    _defaultValueGetter(index) :
                    default(TResult);

                TResult r = default(TResult);
                if (_getterFunction != null)
                {
                    r = _getterFunction(index);
                    if (r != null)
                    {
                        result = r;
                    }
                }
                return result;
            }

            set
            {
                if (_setterAction != null)
                {
                    _setterAction(index, value);
                }
            }
        }

        /// <summary>
        /// Notifies changes (MVVM) for the whole indexer property.
        /// </summary>
        public void NotifyIndexerChanged()
        {
            OnPropertyChanged("Item[]");
        }

        #endregion IFunctionDictionary members

        #region IDictionary members

        public void Add(TKey key, TResult value)
        {
            if (_setterAction != null)
            {
                _setterAction(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            TResult outValue;
            return TryGetValue(key, out outValue);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return new List<TKey>();
            }
        }

        public bool Remove(TKey key)
        {
            // Not implemented
            return false;
        }

        public bool TryGetValue(TKey key, out TResult value)
        {
            TResult defaultResult = _defaultValueGetter != null ?
                    _defaultValueGetter(key) :
                    default(TResult);
            value = defaultResult;

            if (_getterFunction != null)
            {


                if (((value = _getterFunction(key)).Equals(defaultResult)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public ICollection<TResult> Values
        {
            get { return new List<TResult>(); }
        }

        public void Add(KeyValuePair<TKey, TResult> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            // Not implemented.
        }

        public bool Contains(KeyValuePair<TKey, TResult> item)
        {
            TResult outResult;
            if (TryGetValue(item.Key, out outResult))
            {
                if (outResult.Equals(item.Value))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TResult>[] array, int arrayIndex)
        {
            // Not implemented;
        }

        public int Count
        {
            get
            {
                // Not implemented
                return 0;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                // Not implemented
                return false;
            }
        }

        public bool Remove(KeyValuePair<TKey, TResult> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TResult>> GetEnumerator()
        {
            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return null;
        }

        #endregion IDictionary members
    }
}
