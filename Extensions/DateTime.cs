#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        internal static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        /// <summary>
        /// Returns the specified DateTime as epoch (seconds since Jan 1, 1970)
        /// </summary>
        /// <returns>Epoch</returns>
        /// <param name="dt">DateTime to use</param>
        public static long ToEpoch(this DateTime dt) {
            return (long) (dt - Epoch).TotalSeconds;
        }
    }
}