#region
using System;
using System.Collections.Generic;
using System.Linq;
using bscheiman.Common.Helpers;

#endregion

namespace bscheiman.Common.Extensions {
    public static class IEnumerableExtensions {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> pSeq) {
            return pSeq ?? Enumerable.Empty<T>();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            action.ThrowIfNull("action");

            if (source == null)
                return;

            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// Returns a random element from the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Random element</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> list) {
            list.ThrowIfNull("list");

            var tmp = list.ToList();
            int count = tmp.Count();

            return count == 0 ? default(T) : tmp.ElementAt(RandomHelper.Instance.Next(count));
        }

        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> src) {
            return src == null || !src.Any();
        }

        public static string Join<T>(this IEnumerable<T> collection, Func<T, string> func, string separator) {
            return collection.IsNullOrEmpty() ? string.Empty : string.Join(separator, collection.Select(func).ToArray());
        }

        public static string Join<T>(this IEnumerable<T> src, string separator = "") {
            return src.IsNullOrEmpty() ? string.Empty : string.Join(separator, src);
        }
    }
}