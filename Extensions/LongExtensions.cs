#region
using System;
using bscheiman.Common.Util;

#endregion

namespace bscheiman.Common.Extensions {
    public static class LongExtensions {
        /// <summary>
        /// Creates a DateTime object from the specified number of seconds
        /// </summary>
        /// <returns>DateTime object</returns>
        /// <param name="l">Epoch</param>
        public static DateTime FromEpoch(this long l) {
            return DateUtil.Epoch.AddSeconds(l);
        }
    }
}
