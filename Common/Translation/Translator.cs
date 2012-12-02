using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Common.Interfaces.Core.Translation;
using Common.Interfaces.Core.Collections;

namespace Common.Core.Translation
{
    /// <summary>
    /// <para>
    /// The translator class implements the ITranslator interface
    /// for providing translation functions by the indexer property.
    /// </para>
    /// <para>
    /// It also implements IValueConverter interface for giving a 
    /// posibility of binding to it in xaml and providing by the binding
    /// the text resource to be translated.
    /// </para>
    /// <para>
    /// The developer can also pin his own dictionary or function for providing 
    /// the translation.
    /// </para>
    /// </summary>
    public class Translator : Common.Core.MVVM.ViewModelBase, IValueConverter, ITranslator
    {
        #region Default Translator

        /// <summary>
        /// Holds the default translator instance.
        /// </summary>
        private static ITranslator _defaultTranslator;

        /// <summary>
        /// Gets or sets a default translator object which can be used for default 
        /// initialization of objects that use the ITranslator instance to 
        /// translate their content (like controls etc.)
        /// </summary>
        public static ITranslator Default
        {
            get
            {
                if (_defaultTranslator == null)
                {
                    _defaultTranslator = new Translator();
                }

                return _defaultTranslator;
            }

            set
            {
                _defaultTranslator = value;
            }
        }

        #endregion Default Translator
        
        #region Missing keys helper
		
        // This mechanism is a helper for gathering the keys that haven't any translation.

        /// <summary>
        /// Holds keys that don;t have any translation.
        /// </summary>
        private Dictionary<string, string> _requestedIndexes = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets a property indicating whether the localizator is gathering missing keys.
        /// </summary>
        public bool IsGatheringMissingKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a requested key index, if it hasn't any translation.
        /// </summary>
        /// <param name="index"></param>
        protected void AddRequestedIndex(string index)
        {
            if (!_requestedIndexes.ContainsKey(index))
            {
                _requestedIndexes.Add(index, index);
            }
        }

        /// <summary>
        /// Returns all the keys not having any translation, gathered since the application start.
        /// </summary>
        /// <returns>New line separated keys not having any translation.</returns>
        public string GetRequestedIndexes()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var index in _requestedIndexes)
            {
                builder.Append(index.Value);
                builder.Append("\n");
            }

            return builder.ToString();
        }

	    #endregion Missing keys helper

        #region Private fields
		
        /// <summary>
        /// Holds a custom list of translations
        /// </summary>
        private IDictionary<string, string> _customData;

        /// <summary>
        /// Holds a custom function for providing translations.
        /// </summary>
        private Func<string, string> _customTranslationFunction;

	    #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Translator class.
        /// </summary>
        public Translator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Translator class 
        /// with a custom translation function. This unsets custom translation
        /// dictionary if present.
        /// </summary>
        /// <param name="customTranslationEngine">Function for providing 
        /// custom translations by a function.</param>
        public Translator(Func<string, string> customTranslationEngine)
        {
            _customTranslationFunction = customTranslationEngine;
            _customData = null;
        }

        /// <summary>
        /// Initializes a new instance of the Translator class 
        /// with a custom translation dictionary. This unsets 
        /// custom translation function if presetn.
        /// </summary>
        /// <param name="data">Custom dictionary for prociding translations.</param>
        public Translator(IDictionary<string, string> data)
        {
            _customTranslationFunction = null;
            _customData = data;
        }

        #endregion Constructors

        #region Public initialization methods

        /// <summary>
        /// Sets a custom translation function. This unsets custom translation
        /// dictionary if present.
        /// </summary>
        /// <param name="customTranslationEngine">Function for providing custom translations
        /// by a function.</param>
        public void SetTranslationSource(Func<string, string> customTranslationEngine)
        {
            _customTranslationFunction = customTranslationEngine;
            _customData = null;
            NotifyLanguageChanged();
        }

        /// <summary>
        /// Sets a custom translation dictionary. This unsets 
        /// custom translation function if presetn.
        /// </summary>
        /// <param name="data">Custom dictionary for prociding translations.</param>
        public void SetTranslationDictionary(IDictionary<string, string> data)
        {
            _customTranslationFunction = null;
            _customData = data;
            NotifyLanguageChanged();
        }

        #endregion Public initialization methods

        #region Indexer property

        /// <summary>
        /// Gets (setter is only for TwoWay binding purposes) a
        /// translation for the given key.
        /// </summary>
        /// <param name="index">Key for translation.</param>
        /// <returns>Translation if present, or the given key if not present.</returns>
        public string this[string index]
        {
            get
            {
                if (string.IsNullOrEmpty(index))
                {
                    return index;
                }

                index = index.Trim();

                string result = index;

                string r = null;
                if (_customData != null)
                {
                    if (_customData.TryGetValue(index, out r))
                    {
                        result = r;
                    }
                }
                else
                {
                    if (_customTranslationFunction != null)
                    {
                        string custom = _customTranslationFunction(index);
                        if (custom != null)
                        {
                            result = custom;
                        }
                    }
                }

                if (result == null)
                {
                    result = index;

                    if (IsGatheringMissingKeys)
                    {
                        result = "@#" + index;
                        AddRequestedIndex(index);
                    }
                }
                else
                {
                    if (IsGatheringMissingKeys)
                    {
                        result = "@" + result;
                    }
                }

                return result;
            }

            set
            {
                // Setter is public only for purposes of 
                // using TwoWay binding on TextBoxes with readonly state.
            }
        }

        #endregion Indexer property

        #region Language Changed

        /// <summary>
        /// Notifies about change of language.
        /// </summary>
        public void NotifyLanguageChanged()
        {
            OnPropertyChanged("Item[]");
            if (LanguageChanged != null)
            {
                LanguageChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event occurs when the language on the translator changes.
        /// </summary>
        public event EventHandler LanguageChanged;

        #endregion Language Changed

        #region IValueConverter Members

        /// <summary>
        /// Converts a giving value key into it's translation.
        /// </summary>
        /// <param name="value">Key value</param>
        /// <param name="targetType">Type of string.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>A string translation for the given value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return this[value.ToString()];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The back method returns the same thing as in value.
        /// </summary>
        /// <param name="value">Not used in back method.</param>
        /// <param name="targetType">Not used in back method.</param>
        /// <param name="parameter">Not used in back method.</param>
        /// <param name="culture">Not used in back method.</param>
        /// <returns>The value without change.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion IValueConverter Members
    }
}
