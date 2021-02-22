using System;
using System.Collections.Generic;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class ArrayExtensionMethods
    {
        /// <summary>
        /// Returns a list of repeated elements
        /// </summary>
        /// <typeparam name="T">the array's wrapper type</typeparam>
        /// <param name="array">the array of elements</param>
        /// <param name="validator">a predicate used to compare two objects</param>
        /// <returns>array of duplicates or an empty array</returns>
        public static T[] FindDuplicates<T>(this T[] array, Func<T, T, bool> validator)
        {
            var duplicates = new List<T>();

            for (var arrayCounter = 0; arrayCounter < array.Length; arrayCounter++)
            {
                for (var otherCounter = arrayCounter + 1; otherCounter < array.Length; otherCounter++)
                {
                    if (validator(array[arrayCounter], array[otherCounter]))
                    {
                        duplicates.Add(array[arrayCounter]);
                    }
                }
            }
            return duplicates.ToArray();
        }

        /// <summary>
        /// Checks if an array is null or empty
        /// </summary>
        /// <typeparam name="T">the elements' type</typeparam>
        /// <param name="array">the array of elements</param>
        /// <returns>true if the array is null or empty</returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                return true;
            }

            foreach (var type in array)
            {
                if (!type.Equals(default(T)))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Executes a series of actions
        /// </summary>
        public static TResult[] Execute<TSource, TResult>(this TSource[] array, Func<TSource, TResult> function)
        {
            var result = new List<TResult>();

            if (array.IsNullOrEmpty())
            {
                return default;
            }

            foreach (var item in array)
            {
                result.Add(function.Invoke(item));
            }
            return result.ToArray();
        }
    }
}
