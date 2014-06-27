#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        /// <summary>
        /// Creates a DateTime object from the specified number of seconds
        /// </summary>
        /// <returns>DateTime object</returns>
        /// <param name="l">Epoch</param>
        public static DateTime FromEpoch(this long l) {
            return Epoch.AddSeconds(l);
        }

        /// <summary>
        /// Returns the specified number of bytes as gigabytes
        /// </summary>
        /// <returns>Expressed gigabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToGb(this long l) {
            return l.ToMb() / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as kilobytes
        /// </summary>
        /// <returns>Expressed kilobytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToKb(this long l) {
            return l / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as megabytes
        /// </summary>
        /// <returns>Expressed megabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToMb(this long l) {
            return l.ToKb() / 1024f;
        }

        /// <summary>
        /// Returns the specified number of bytes as terabytes
        /// </summary>
        /// <returns>Expressed terabytes</returns>
        /// <param name="l">Number of bytes</param>
        public static float ToTb(this long l) {
            return l.ToGb() / 1024f;
        }
    }
}