using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Translation
{
    public class TranslationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new attribute with the given translation key.
        /// </summary>
        /// <param name="translationKey">Translation key.</param>
        public TranslationAttribute(string translationKey)
        {
            TranslationKey = translationKey;
        }

        /// <summary>
        /// Gets or sets the translation key used for the attribute target.
        /// </summary>
        public string TranslationKey
        {
            get;
            set;
        }
    }
}
