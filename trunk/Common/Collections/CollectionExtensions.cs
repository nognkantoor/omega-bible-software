using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Common.Core.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Performs an action on each element of the collection.
        /// </summary>
        /// <typeparam name="TElement">Collection item.</typeparam>
        /// <param name="element">Collection to be used.</param>
        /// <param name="action">Action to be executed on each item of the collection.</param>
        public static void Each<TElement>(this ICollection<TElement> element, Action<TElement> action)
        {
            if (element != null && action != null)
            {
                foreach (var e in element)
                {
                    action(e);
                }
            }
        }

        /// <summary>
        /// Gets the value for the given key. This operation is null safe, and returns 
        /// the default TValue value if the key isn't present in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary.</typeparam>
        /// <param name="dictionary">Dictionary to get the key from.</param>
        /// <param name="key">Key to search.</param>
        /// <returns>Value under the given key, or default value for TValue type, 
        /// if the key is not present in the dictionary.</returns>
        public static TValue GetForKey<TKey,TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue v = default(TValue);
            if (dictionary == null) return v;
            if (dictionary.TryGetValue(key, out v))
            {
                return v;
            }
            return v;
        }

        /// <summary>
        /// Takes a part of the collection between the given range. 
        /// </summary>
        /// <param name="collection">Collection to get from.</param>
        /// <param name="from">Begining index to get from (inclusive).</param>
        /// <param name="to">End index to get from (inclusive).</param>
        /// <returns>Collection made out of the elements between the given indexes.</returns>
        public static IEnumerable TakePart(this IEnumerable collection, int from, int to)
        {
            List<object> result = new List<object>();
            int index = 0;
            foreach (object v in collection)
            {
                if (index >= from && index <= to) result.Add(v);
            }
            return result;
        }

        /// <summary>
        /// Takes a part of the collection between the given range. 
        /// </summary>
        /// <param name="collection">Collection to get from.</param>
        /// <param name="from">Begining index to get from (inclusive).</param>
        /// <param name="to">End index to get from (inclusive).</param>
        /// <returns>Collection made out of the elements between the given indexes.</returns>
        public static IList<T> TakePart<T>(this IList<T> collection, int from, int to)
        {
            IList<T> result = new List<T>();
            int index = 0;
            foreach (T v in collection)
            {
                if (index >= from && index <= to) result.Add(v);
            }
            return result;
        }

    }
}
