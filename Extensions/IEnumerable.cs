#region
using System.Collections.Generic;
using System.Linq;
using bscheiman.Common.Helpers;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        /// <summary>
        /// Returns a random element from the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Random element</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> list) {
            var tmp = list.ToList();
            var count = tmp.Count();

            return count == 0 ? default(T) : tmp.ElementAt(RandomHelper.Instance.Next(count));
        }
    }
}