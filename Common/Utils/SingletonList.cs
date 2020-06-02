using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utils
{
    public static class SingletonList
    {
        /// <summary>
        /// Creates a readonly list with a single value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value</param>
        /// <returns></returns>
        public static IList<T> Of<T>(T value) => new List<T> { value }.AsReadOnly();
    }

}
