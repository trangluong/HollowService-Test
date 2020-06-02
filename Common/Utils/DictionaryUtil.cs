using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Common.Utils
{
    public static class DictionaryUtil
    {
        /// <summary>
        /// Get a value from a dictionary or return the default for the value type if the value is not found.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">the value to use if not found in the dictionary</param>
        /// <returns></returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default(TValue))
        {
            var value = default(TValue);
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Adds all the entries in a <see cref="IEnumerable{T}"/> to the target <see cref="ICollection{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            ContractHelper.RequiresNotNull(target);
            foreach (var element in ContractHelper.RequiresNotNull(source))
                target.Add(element);

        }

        /// <summary>
        /// Filter a dictionary by a list of keys
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="filter"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void Filter<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> filter)
        {
            if (filter == null)
            {
                dictionary.Clear();
                return;
            }

            foreach (var item in dictionary.Where(w => !filter.Contains(w.Key)).ToList())
                dictionary.Remove(item.Key);

        }
    }
}
