using System;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace bscheiman.Common.Helpers {
	public static class HashHelper {
		/// <summary>
		/// Calculates the HMAC-SHA256 hash for a specific key + timestamp + parameters
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="key">Key, usually API secret</param>
		/// <param name="timestamp">Timestamp (epoch)</param>
		/// <param name="separator">Separator, defaults to "</param>
		/// <param name="parms">Parameters to use</param>
		public static string CalculateHmacSha256(string key, long timestamp, string separator = ":", params string[] parms) {
			using (var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
				return CalculateHmac(hmac, timestamp, separator, parms);
		}

		/// <summary>
		/// Calculates the HMAC-MD5 hash for a specific key + timestamp + parameters
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="key">Key, usually API secret</param>
		/// <param name="timestamp">Timestamp (epoch)</param>
		/// <param name="separator">Separator, defaults to "</param>
		/// <param name="parms">Parameters to use</param>
		public static string CalculateHmacMd5(string key, long timestamp, string separator = ":", params string[] parms) {
			using (var hmac = new HMACMD5(Encoding.ASCII.GetBytes(key)))
				return CalculateHmac(hmac, timestamp, separator, parms);
		}

		/// <summary>
		/// Calculates the HMAC-SHA1 hash for a specific key + timestamp + parameters
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="key">Key, usually API secret</param>
		/// <param name="timestamp">Timestamp (epoch)</param>
		/// <param name="separator">Separator, defaults to "</param>
		/// <param name="parms">Parameters to use</param>
		public static string CalculateHmacSha1(string key, long timestamp, string separator = ":", params string[] parms) {
			using (var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(key)))
				return CalculateHmac(hmac, timestamp, separator, parms);
		}

		/// <summary>
		/// Calculates the HMAC-SHA384 hash for a specific key + timestamp + parameters
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="key">Key, usually API secret</param>
		/// <param name="timestamp">Timestamp (epoch)</param>
		/// <param name="separator">Separator, defaults to "</param>
		/// <param name="parms">Parameters to use</param>
		public static string CalculateHmacSha384(string key, long timestamp, string separator = ":", params string[] parms) {
			using (var hmac = new HMACSHA384(Encoding.ASCII.GetBytes(key)))
				return CalculateHmac(hmac, timestamp, separator, parms);
		}

		/// <summary>
		/// Calculates the HMAC-SHA512 hash for a specific key + timestamp + parameters
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="key">Key, usually API secret</param>
		/// <param name="timestamp">Timestamp (epoch)</param>
		/// <param name="separator">Separator, defaults to "</param>
		/// <param name="parms">Parameters to use</param>
		public static string CalculateHmacSha512(string key, long timestamp, string separator = ":", params string[] parms) {
			using (var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(key)))
				return CalculateHmac(hmac, timestamp, separator, parms);
		}

		internal static string CalculateHmac(HMAC provider, long timestamp, string separator = ":", params string[] parms) {
			var list = new List<string> { timestamp.ToString(CultureInfo.InvariantCulture) };
			list.AddRange(parms);

			return BitConverter.ToString(provider.ComputeHash(Encoding.ASCII.GetBytes(string.Join(separator, list)))).Replace("-", "").ToUpper();
		}
	}
}

