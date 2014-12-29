#region
using System;
using System.Collections.Generic;
using bscheiman.Common.Util;

#endregion

namespace bscheiman.Common.Extensions {
    public static class IntExtensions {
        /// <summary>
        /// Returns number of days in seconds.
        /// </summary>
        /// <param name="i">Days</param>
        public static long Days(this int i) {
            return i * 24.Hours();
        }

        /// <summary>
        /// Creates a DateTime object from the specified number of seconds
        /// </summary>
        /// <returns>DateTime object</returns>
        /// <param name="l">Epoch</param>
        public static DateTime FromEpoch(this int l) {
            return DateUtil.Epoch.AddSeconds(l);
        }

        /// <summary>
        /// Returns number of hours in seconds. (ie, 60*i)
        /// </summary>
        /// <param name="i">Hours</param>
        public static long Hours(this int i) {
            return i * 60.Minutes();
        }

        /// <summary>
        /// Returns number of minutes in seconds. (ie, 60*i)
        /// </summary>
        /// <param name="i">Minutes</param>
        public static long Minutes(this int i) {
            return i * 60.Seconds();
        }

        public static IEnumerable<int> Range(this int n) {
            for (int i = 0; i < n; i++)
                yield return i;
        }

        /// <summary>
        /// Returns number of seconds. IN SECONDS. Best extension ever.
        /// </summary>
        /// <param name="i">Seconds</param>
        public static long Seconds(this int i) {
            return i;
        }

        /// <summary>
        /// Returns the specified number of bytes as gigabytes
        /// </summary>
        /// <returns>Expressed gigabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToGb(this int l) {
            return l.ToMb() / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as kilobytes
        /// </summary>
        /// <returns>Expressed kilobytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToKb(this int l) {
            return l / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as megabytes
        /// </summary>
        /// <returns>Expressed megabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToMb(this int l) {
            return l.ToKb() / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as terabytes
        /// </summary>
        /// <returns>Expressed terabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToTb(this int l) {
            return l.ToGb() / 1024f;
        }

        /// <summary>
        /// Returns number of years in seconds. (ie, 60*i)
        /// </summary>
        /// <param name="i">Years</param>
        public static long Years(this int i) {
            return i * 365.Days();
        }
    }
}