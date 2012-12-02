using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Controls.Forms
{
    /// <summary>
    /// Defines a control with localizable capabilities through the TranslationKey property.
    /// </summary>
    public interface ILocalizableControl
    {
        /// <summary>
        /// Gets or sets the translation key.
        /// </summary>
        string TranslationKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the translator that makes the localization of text.
        /// </summary>
        Common.Interfaces.Core.Translation.ITranslator Translator
        {
            get;
            set;
        }
    }
}
