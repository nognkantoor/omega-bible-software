using System;
using Common.Interfaces.Controls.Forms;
using Common.Interfaces.Core.Translation;

namespace Common.Controls.Forms
{
    /// <summary>
    /// ExtendedLabel extends the standard System.Windows.Forms.Label
    /// with additional capabilities:
    /// <list type="bullet">
    /// <item>translating label text (through TextTranslation property)</item>
    /// </list>
    /// </summary>
    public class ExtendedLabel : System.Windows.Forms.Label, ILocalizableControl
    {
        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the ExtendedLabel class.
        /// </summary>
        public ExtendedLabel()
        {
            Translator = Common.Core.Translation.Translator.Default;
        }

        #endregion Constuctors

        #region Private fields

        /// <summary>
        /// Holds translation key for translating and writing into the label text.
        /// </summary>
        private string _textTranslation;

        /// <summary>
        /// Holds a translator instance.
        /// </summary>
        private ITranslator _translator; 

        #endregion Private fields

        #region Private methods

        /// <summary>
        /// Refreshes the label text with the translation.
        /// </summary>
        private void RefreshTranslations()
        {
            if (!string.IsNullOrEmpty(TranslationKey))
            {
                Text = GetTranslation(TranslationKey);
            }
        }

        /// <summary>
        /// Gets a translation for the given key
        /// </summary>
        /// <param name="key">Translation key</param>
        /// <returns>Translated key, if translator is present.</returns>
        private string GetTranslation(string key)
        {
            if (Translator != null)
            {
                return Translator[key];
            }
            //else
            return key;
        }

        /// <summary>
        /// Handles the event for changing in language in the translator.
        /// </summary>
        /// <param name="sender">ITranslator instance</param>
        /// <param name="args">Empty arguments</param>
        private void LanguageChanged(object sender, EventArgs args)
        {
            RefreshTranslations();
        }

        #endregion Private methods

        #region Public properties

        /// <summary>
        /// Gets or sets the text being the key of translation.
        /// </summary>
        public string TranslationKey
        {
            get
            {
                return _textTranslation;
            }

            set
            {
                _textTranslation = value;
                RefreshTranslations();
            }
        }

        /// <summary>
        /// Gets or sets a translator instance.
        /// </summary>
        public virtual ITranslator Translator
        {
            get { return _translator; }
            set
            {
                if (_translator != null)
                {
                    _translator.LanguageChanged -= LanguageChanged;
                }
                _translator = value;
                if (_translator != null)
                {
                    _translator.LanguageChanged += LanguageChanged;
                }

                RefreshTranslations();
            }
        }

        #endregion Public properties
    }
}
