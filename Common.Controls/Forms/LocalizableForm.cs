﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Common.Interfaces.Core.Translation;
using System.ComponentModel;
using Common.Interfaces.Controls.Forms;

namespace Common.Controls.Forms
{
    public class LocalizableForm : System.Windows.Forms.Form, ILocalizableControl
    {
        private string _textTranslation;
        private ITranslator _translator;

        /// <summary>
        /// Gets or sets the key index for translating into the Text value.
        /// </summary>
        [Category("Appearance")]
        [Description("The key text for translating into Text (titlebar of the window).")]
        public string TextTranslation
        {
            get
            {
                return _textTranslation;
            }

            set
            {
                _textTranslation = value;
                Text = GetTranslation(value);
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
