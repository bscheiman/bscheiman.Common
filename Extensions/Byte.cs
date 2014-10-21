#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        /// <summary>
        /// Converts a byte array to its hex string representation.
        /// </summary>
        /// <param name="byteArray">Byte array</param>
        /// <returns>A hex string</returns>
        public static string ToHexString(this byte[] byteArray) {
            return BitConverter.ToString(byteArray).Replace("-", "");
        }
    }
}