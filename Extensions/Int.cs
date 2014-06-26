using System;

namespace bscheiman.Common.Extensions {
	public static partial class Extensions {
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
			return Epoch.AddSeconds(l);
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

		/// <summary>
		/// Returns number of seconds. IN SECONDS. Best extension ever.
		/// </summary>
		/// <param name="i">Seconds</param>
		public static long Seconds(this int i) {
			return i;
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

