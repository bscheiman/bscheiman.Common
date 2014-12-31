#region
using System;
using System.Collections.Generic;
using bscheiman.Common.Util;

#endregion

namespace bscheiman.Common.Extensions {
    public static class IntExtensions {
        /// <summary>
        /// Creates a DateTime object from the specified number of seconds
        /// </summary>
        /// <returns>DateTime object</returns>
        /// <param name="l">Epoch</param>
        public static DateTime FromEpoch(this int l) {
            return DateUtil.Epoch.AddSeconds(l);
        }

        public static IEnumerable<int> Range(this int n, int max) {
            for (int i = n; i < n + max; i++)
                yield return i;
        }

        public static IEnumerable<int> RangeInclusive(this int n, int max) {
            for (int i = n; i <= n + max; i++)
                yield return i;
        }
    }
}