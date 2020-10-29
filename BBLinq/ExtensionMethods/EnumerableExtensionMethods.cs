using System.Collections.Generic;
using System.Linq;

namespace BBLinq.ExtensionMethods
{
    internal static class EnumerableExtensionMethods
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Count() == 0;
        }
    }
}
