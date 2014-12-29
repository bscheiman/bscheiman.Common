#region
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

#endregion

namespace bscheiman.Common.Extensions {
    public static class StringExtensions {
        /// <summary>
        /// Attempts to parse the specified string as a member of enum
        /// </summary>
        /// <returns>The parsed enum.</returns>
        /// <param name="str">String to parse, case insensitive.</param>
        /// <typeparam name="T">Type of enum</typeparam>
        public static T AsEnum<T>(this string str) {
            return (T) Enum.Parse(typeof (T), str, true);
        }

        public static string FormatWith(this string str, params object[] args) {
            return string.Format(str, args);
        }

        public static string FormatWith<T>(this string format, Func<T, object> select, params object[] args) where T : class {
            for (int i = 0; i < args.Length; ++i) {
                var x = args[i] as T;
                if (x != null)
                    args[i] = select(x);
            }

            return string.Format(format, args);
        }

        /// <summary>
        /// Returns a byte array representing the specified string. ("0000" => 0x00, 0x00)
        /// </summary>
        /// <returns>The hex string.</returns>
        /// <param name="str">String to convert.</param>
        public static byte[] FromHexString(this string str) {
            int numberChars = str.Length;
            var bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);

            return bytes;
        }

        /// <summary>
        /// Converts a JSON string to its object counterpart
        /// </summary>
        /// <typeparam name="T">Type to deserialize</typeparam>
        /// <param name="str">JSON string</param>
        /// <returns>A plain ol' .NET object</returns>
        public static T FromJson<T>(this string str) {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static bool IsLike(this string s, string wildcardPattern) {
            if (s == null || String.IsNullOrEmpty(wildcardPattern))
                return false;

            string regexPattern =
                string.Format("^{0}$", Regex.Escape(wildcardPattern))
                      .Replace(@"\[!", "[^")
                      .Replace(@"\[", "[")
                      .Replace(@"\]", "]")
                      .Replace(@"\?", ".")
                      .Replace(@"\*", ".*")
                      .Replace(@"\#", @"\d");

            try {
                return Regex.IsMatch(s, regexPattern);
            } catch (ArgumentException ex) {
                throw new ArgumentException(String.Format("Invalid pattern: {0}", wildcardPattern), ex);
            }
        }

        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        public static bool MatchesWildcard(this string text, string pattern) {
            int it = 0;

            while (text.CharAt(it) != 0 && pattern.CharAt(it) != '*') {
                if (pattern.CharAt(it) != text.CharAt(it) && pattern.CharAt(it) != '?')
                    return false;

                it++;
            }

            int cp = 0;
            int mp = 0;
            int ip = it;

            while (text.CharAt(it) != 0) {
                if (pattern.CharAt(ip) == '*') {
                    if (pattern.CharAt(++ip) == 0)
                        return true;
                    mp = ip;
                    cp = it + 1;
                } else if (pattern.CharAt(ip) == text.CharAt(it) || pattern.CharAt(ip) == '?') {
                    ip++;
                    it++;
                } else {
                    ip = mp;
                    it = cp++;
                }
            }

            while (pattern.CharAt(ip) == '*')
                ip++;

            return pattern.CharAt(ip) == 0;
        }

        /// <summary>
        /// Removes the diacritics from a UTF8 string. Use with caution: in Spanish, año is *VERY* different from ano.
        /// </summary>
        /// <returns>Diacritic-less string.</returns>
        /// <param name="input">String to modify.</param>
        public static string RemoveDiacritics(this string input) {
            string stFormD = input.Normalize(NormalizationForm.FormD);
            int len = stFormD.Length;
            var sb = new StringBuilder();

            for (int i = 0; i < len; i++) {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);

                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[i]);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static IEnumerable<string> SplitInParts(this string s, int partLength) {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (int i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static string[] SplitRe(this string value, string regexPattern, RegexOptions options = RegexOptions.None) {
            return Regex.Split(value, regexPattern, options);
        }

        public static T To<T>(this IConvertible obj) {
            return (T) Convert.ChangeType(obj, typeof (T));
        }

        /// <summary>
        /// Generates an HMAC SHA256 string from the specified string and key
        /// </summary>
        /// <returns>Hashed string, uppercase.</returns>
        /// <param name="str">Source string.</param>
        /// <param name="key">Key.</param>
        public static string ToHMAC256(this string str, string key) {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Generates an MD5 hash from the specified string
        /// </summary>
        /// <returns>MD5 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToMD5(this string str) {
            using (var sha = MD5.Create())
                return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Generates a SHA1 hash from the specified string
        /// </summary>
        /// <returns>SHA1 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToSHA1(this string str) {
            using (var sha = SHA1.Create())
                return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Generates a SHA256 hash from the specified string
        /// </summary>
        /// <returns>SHA256 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToSHA256(this string str) {
            using (var sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Truncate the specified string according to maxLength.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="maxLength">Max length.</param>
        public static string Truncate(this string value, int maxLength) {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        internal static char CharAt(this string s, int index) {
            return index < s.Length ? s[index] : '\0';
        }
    }
}