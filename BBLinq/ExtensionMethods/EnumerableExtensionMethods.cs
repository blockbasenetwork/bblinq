using System.Collections.Generic;
using System.Linq;

namespace agap2IT.Labs.BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Extension Methods for the Enumerable<T> type
    /// </summary>
    internal static class EnumerableExtensionMethods
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
