using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Common.Interfaces.Core.Translation;
using System.ComponentModel;
using Common.Interfaces.Controls.Forms;
using Common.Controls.Windows;

namespace Common.Controls.Forms
{
    /// <summary>
    /// ExtendedForm extends the standard System.Windows.Forms.Form
    /// with additional capabilities:
    /// <list type="bullet">
    /// <item>Translated title trough the TextTranslation property.</item>
    /// </list>
    /// </summary>
    public class ExtendedForm : System.Windows.Forms.Form, ILocalizableControl
    {
        #region Private fields

        private string _translationKey;
        private ITranslator _translator;

        #endregion Private fields

        #region Constructors

        public ExtendedForm()
        {
            Translator = Common.Core.Translation.Translator.Default;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Handles the event for changing in language in the translator.
        /// </summary>
        /// <param name="sender">ITranslator instance</param>
        /// <param name="args">Empty arguments</param>
        private void LanguageChanged(object sender, EventArgs args)
        {
            RefreshTranslations();
        }

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

        #endregion Private methods

        #region Protected methods

        /// <summary>
        /// Processes the window message.
        /// </summary>
        /// <param name="message">Window message</param>
        protected override void WndProc(ref Message message)
        {
            if (ExternalMessageComming != null)
            {
                WndProcMessageEventArgs args = new WndProcMessageEventArgs(message);
                ExternalMessageComming(this, args);

                base.WndProc(ref message);
            }
            else
            {
                base.WndProc(ref message);
            }
        }
        
        #endregion Protected methods

        #region Public properties

        #region ILocalizableControl members

        /// <summary>
        /// Gets or sets the key index for translating into the Text value.
        /// </summary>
        [Category("Appearance")]
        [Description("The key text for translating into Text (titlebar of the window).")]
        public string TranslationKey
        {
            get
            {
                return _translationKey;
            }

            set
            {
                _translationKey = value;
                Text = GetTranslation(value);
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

        #endregion ILocalizableControl members

        #endregion Public properties

        #region Events

        /// <summary>
        /// Event for an external window message comming to this form.
        /// </summary>
        public event EventHandler<WndProcMessageEventArgs> ExternalMessageComming;

        #endregion Events


    }
}
