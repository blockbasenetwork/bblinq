using System.Collections.Generic;
using System.Linq;

namespace agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension Methods for the Enumerable type
    /// </summary>
    internal static class EnumerableExtensionMethods
    {
        /// <summary>
        /// Check if the enumerable is null or empty
        /// </summary>
        /// <typeparam name="T">a generic type accepted by enumerable types</typeparam>
        /// <param name="enumerable">an enumerable collection</param>
        /// <returns></returns>
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
