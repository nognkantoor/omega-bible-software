using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Interfaces.Core.Translation;

namespace Common.Controls.Forms
{
    public class ExtendedLabel : System.Windows.Forms.Label
    {
        private string _textTranslation;
        private ITranslator _translator;

        public string TextTranslation
        {
            get
            {
                return _textTranslation;
            }

            set
            {
                _textTranslation = value;

            }
        }

        public virtual ITranslator Translator
        {
            get { return _translator; }
            set
            {
                _translator = value;
                RefreshTranslations();
            }
        }

        protected virtual void RefreshTranslations()
        {
            if (!string.IsNullOrEmpty(TextTranslation))
            {
                Text = GetTranslation(TextTranslation);
            }
        }

        protected virtual string GetTranslation(string key)
        {
            if (Translator != null)
            {
                return Translator[key];
            }
            //else
            return key;
        }

    }
}
