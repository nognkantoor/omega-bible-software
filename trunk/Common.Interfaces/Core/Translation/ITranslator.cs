using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Core.Translation
{
    public interface ITranslator
    {
        /// <summary>
        /// Gets or sets a property indicating whether the localizator is gathering missing keys.
        /// </summary>
        bool IsGatheringMissingKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Returns all the keys not having any translation, gathered since the application start.
        /// </summary>
        /// <returns>New line separated keys not having any translation.</returns>
        string GetRequestedIndexes();

        void SetTranslationDictionary(IDictionary<string, string> source);
        void SetTranslationSource(Func<string, string> source);
        event EventHandler LanguageChanged;
        string this[string index] { get; }
    }
}
