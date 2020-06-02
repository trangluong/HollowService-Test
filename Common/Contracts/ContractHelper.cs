using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common.Contracts
{
    public static class ContractHelper
    {
        /// <summary>
        /// Wrapper method to check object is not null using contracts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">the object that cannot be null</param>
        /// <returns>the object if it is not null</returns>
        public static T RequiresNotNull<T>(T o)
        {
            Contract.Requires(o != null);
            return o;
        }

        /// <summary>
        /// Wrapper method to check if a list of objects contains no nulls using contracts.
        /// </summary>
        /// <typeparam name="T">The type of object in the list.</typeparam>
        /// <param name="items">A list of items to check. If null, this will pass.</param>
        /// <returns>The list that was passed in.</returns>
        public static IList<T> RequiresContainsNoNulls<T>(IList<T> items)
        {
            Contract.Requires(items?.All(i => i != null) ?? true);
            return items;
        }

        /// <summary>
        /// Wrapper method to check if a list of integers contains all positives.
        /// </summary>
        /// <param name="items">A list of ints to check. If null, this will pass.</param>
        /// <returns>The list that was passed in.</returns>
        public static IList<int> RequiresContainsAllPositive(IList<int> items)
        {
            Contract.Requires(items?.All(i => i > 0) ?? true);
            return items;
        }

        public static string RequiresNotNullOrWhitespace(string value)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value));
            return value;
        }

        /// <summary>
        /// Asserts that the specified string has the specified length and is not whitespace
        /// </summary>
        /// <param name="value">the string value</param>
        /// <param name="length">the required length</param>
        /// <returns>the specified string if it is valid, otherwise an exception is thrown</returns>
        public static string RequiresLength(string value, int length)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value) && value.Length == length);
            return value;
        }

        /// <summary>
        /// Asserts that the specified value is a positive. Zero is treated as positive.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the value if it is positive</returns>
        public static decimal RequiresPositive(decimal value)
        {
            Contract.Requires(value >= 0);
            return value;
        }

        /// <summary>
        /// Asserts that the specified integer is greater than zero
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>the value if it is greater than zero</returns>
        public static int RequiresGreaterThanZero(int value)
        {
            Contract.Requires(value > 0);
            return value;
        }

        /// <summary>
        /// Asserts that the specified decimal is greater than zero
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>the value if it is greater than zero</returns>
        public static decimal RequiresGreaterThanZero(decimal value)
        {
            Contract.Requires(value > 0);
            return value;
        }

        /// <summary>
        /// Asserts that the specified integer is less than zero
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>the value if it is less than zero</returns>
        public static decimal RequiresLessThanZero(decimal value)
        {
            Contract.Requires(value < 0);
            return value;
        }

        /// <summary>
        /// Asserts that the specified time span is greater than zero
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>the value if it is greater than zero</returns>
        public static TimeSpan RequiresGreaterThanZero(TimeSpan value)
        {
            Contract.Requires(value > TimeSpan.Zero);
            return value;
        }

        /// <summary>
        /// Asserts that the specified value is >= 0
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the value if >= 0, otherwise throws exception</returns>
        public static int RequiresPositive(int value)
        {
            Contract.Requires(value >= 0);
            return value;
        }

        /// <summary>
        /// Asserts that the specified IEnumerable is not null and is not empty
        /// </summary>
        /// <param name="enumerable">the enumerable to check</param>
        /// <returns>the enumerable if not empty</returns>
        public static IEnumerable<T> RequiresNotNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            Contract.Requires(enumerable?.Any() ?? false);
            return enumerable;
        }

        /// <summary>
        /// Asserts that the specified start is before or equal to end
        /// </summary>
        /// <param name="start">start of the range</param>
        /// <param name="end">end of the range</param>
        public static void RequiresDateRange(DateTimeOffset start, DateTimeOffset end)
        {
            Contract.Requires(start <= end);
        }

        /// <summary>
        /// Ensures that the specified value is &gt;= min and &lt;= max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static decimal RequiresRange(decimal value, decimal min, decimal max)
        {
            Contract.Requires(value >= min && value <= max);
            return value;
        }

        /// <summary>
        /// Ensure that objects are equal.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void RequiresEquals(object first, object second)
        {
            Contract.Requires((first == null && second == null) || (first?.Equals(second) ?? false));
        }

        /// <summary>
        /// Automatically fails a contract. Should be use in method stubs that should never be called.
        /// </summary>
        public static void Fail() => Contract.Assert(false);

        /// <summary>
        /// Requires that a <paramref name="condition"/> be true.
        /// </summary>
        /// <param name="condition">Condition to evaluate.</param>
        public static void RequiresTrue(bool condition) => Contract.Requires(condition);
    }
}
