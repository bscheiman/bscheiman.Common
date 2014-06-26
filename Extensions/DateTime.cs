using System;

namespace bscheiman.Common.Extensions {
	public static partial class Extensions {
		internal static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Returns the specified DateTime as epoch (seconds since Jan 1, 1970)
		/// </summary>
		/// <returns>Epoch</returns>
		/// <param name="dt">DateTime to use</param>
		public static long ToEpoch(this DateTime dt) {
			return Convert.ToInt64((dt - Epoch).TotalSeconds);
		}
	}
}

