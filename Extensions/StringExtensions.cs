#region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

#endregion

namespace bscheiman.Common.Extensions {
    public static class StringExtensions {
        /// <summary>
        /// Attempts to parse the specified string as a member of enum
        /// </summary>
        /// <returns>The parsed enum.</returns>
        /// <param name="str">String to parse, case insensitive.</param>
        /// <typeparam name="T">Type of enum</typeparam>
        [DebuggerStepThrough]
        public static T AsEnum<T>(this string str) {
            str.ThrowIfNull("str");

            return (T) Enum.Parse(typeof (T), str, true);
        }

        [DebuggerStepThrough]
        public static bool CIContains(this string str, string other) {
            str.ThrowIfNullOrEmpty("str");
            other.ThrowIfNullOrEmpty("other");

            return str.ToLower().Contains(other.ToLower());
        }

        [DebuggerStepThrough]
        public static bool CIEndsWith(this string str, string other) {
            str.ThrowIfNullOrEmpty("str");
            other.ThrowIfNullOrEmpty("other");

            return str.ToLower().EndsWith(other.ToLower());
        }

        [DebuggerStepThrough]
        public static bool CIEquals(this string str, string other) {
            str.ThrowIfNullOrEmpty("str");
            other.ThrowIfNullOrEmpty("other");

            return str.Equals(other, StringComparison.CurrentCultureIgnoreCase);
        }

        [DebuggerStepThrough]
        public static int CIIndexOf(this string str, string other) {
            str.ThrowIfNullOrEmpty("str");
            other.ThrowIfNullOrEmpty("other");

            return str.IndexOf(other, StringComparison.CurrentCultureIgnoreCase);
        }

        [DebuggerStepThrough]
        public static bool CIStartsWith(this string str, string other) {
            str.ThrowIfNullOrEmpty("str");
            other.ThrowIfNullOrEmpty("other");

            return str.ToLower().StartsWith(other.ToLower());
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string str, params object[] args) {
            str.ThrowIfNull("str");
            args.ThrowIfNull("args");

            return string.Format(str, args);
        }

        /// <summary>
        /// Returns a byte array representing the specified string. ("0000" => 0x00, 0x00)
        /// </summary>
        /// <returns>The hex string.</returns>
        /// <param name="str">String to convert.</param>
        [DebuggerStepThrough]
        public static byte[] FromHexString(this string str) {
            str.ThrowIfNull("str");

            int outputLength = str.Length / 2;
            var output = new byte[outputLength];

            for (int i = 0; i < outputLength; i++)
                output[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);

            return output;
        }

        /// <summary>
        /// Converts a JSON string to its object counterpart
        /// </summary>
        /// <typeparam name="T">Type to deserialize</typeparam>
        /// <param name="str">JSON string</param>
        /// <returns>A plain ol' .NET object</returns>
        [DebuggerStepThrough]
        public static T FromJson<T>(this string str) {
            str.ThrowIfNull("str");

            return JsonConvert.DeserializeObject<T>(str);
        }

        [DebuggerStepThrough]
        public static byte[] GetBytes(this string str) {
            str.ThrowIfNull("str");

            return str.GetBytes(Encoding.UTF8);
        }

        [DebuggerStepThrough]
        public static byte[] GetBytes(this string str, Encoding encoding) {
            str.ThrowIfNull("str");
            encoding.ThrowIfNull("encoding");

            return encoding.GetBytes(str);
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty(this string str, Action act) {
            if (str.IsNullOrEmpty())
                act();
        }

        [DebuggerStepThrough]
        public static bool IsLike(this string s, string regexPattern) {
            s.ThrowIfNull("s");
            regexPattern.ThrowIfNull("regexPattern");

            try {
                return Regex.IsMatch(s, regexPattern);
            } catch (ArgumentException ex) {
                throw new ArgumentException("Invalid pattern: {0}".FormatWith(regexPattern), ex);
            }
        }

        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty(this string str) {
            return !string.IsNullOrEmpty(str);
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        [DebuggerStepThrough]
        public static bool MatchesWildcard(this string text, string pattern) {
            text.ThrowIfNull("text");
            pattern.ThrowIfNull("pattern");

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

        [DebuggerStepThrough]
        public static IEnumerable<string> SplitInParts(this string s, int partLength) {
            s.ThrowIfNull("s");
            partLength.ThrowIf(partLength <= 0, "Part length has to be positive.", "partLength");

            for (int i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        [DebuggerStepThrough]
        public static string[] SplitRe(this string value, string regexPattern, RegexOptions options = RegexOptions.None) {
            value.ThrowIfNull("value");
            regexPattern.ThrowIfNull("regexPattern");

            return Regex.Split(value, regexPattern, options);
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> SplitRemoveEmptyEntries(this string str) {
            return str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray());
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> SplitRemoveEmptyEntries(this string str, char separator) {
            return str.SplitRemoveEmptyEntries(new[] {
                separator
            });
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> SplitRemoveEmptyEntries(this string str, char[] separator) {
            str.ThrowIfNull("str");
            separator.ThrowIfNull("separator");

            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        [DebuggerStepThrough]
        public static void ThrowIfNullOrEmpty(this string str, string argName) {
            if (str.IsNullOrEmpty())
                throw new ArgumentException(argName);
        }

        [DebuggerStepThrough]
        public static bool ToBool(this string s) {
            if (s.IsIn("1", "t", "T", "y", "Y"))
                return true;

            if (s.IsIn("0", "", "f", "F", "n", "N"))
                return false;

            bool retInt;
            bool.TryParse(s, out retInt);

            return retInt;
        }

        [DebuggerStepThrough]
        public static decimal ToDecimal(this string s) {
            decimal retInt;
            decimal.TryParse(s, out retInt);

            return retInt;
        }

        [DebuggerStepThrough]
        public static double ToDouble(this string s) {
            double retInt;
            double.TryParse(s, out retInt);

            return retInt;
        }

        /// <summary>
        /// Generates an HMAC SHA256 string from the specified string and key
        /// </summary>
        /// <returns>Hashed string, uppercase.</returns>
        /// <param name="str">Source string.</param>
        /// <param name="key">Key.</param>
        public static string ToHMAC256(this string str, string key) {
            return str.ToHMAC256(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// Generates an HMAC SHA256 string from the specified string and key
        /// </summary>
        /// <returns>Hashed string, uppercase.</returns>
        /// <param name="str">Source string.</param>
        /// <param name="key">Key.</param>
        public static string ToHMAC256(this string str, byte[] key) {
            str.ThrowIfNull("str");
            key.ThrowIfNull("key");

            var digest = new Sha256Digest();
            var hmac = new HMac(digest);
            hmac.Init(new KeyParameter(key));

            foreach (byte b in Encoding.UTF8.GetBytes(str))
                hmac.Update(b);

            var resBuf = new byte[digest.GetDigestSize()];
            hmac.DoFinal(resBuf, 0);

            return BitConverter.ToString(resBuf).Replace("-", "").ToUpper();
        }

        [DebuggerStepThrough]
        public static int ToInt32(this string s) {
            int retInt;
            int.TryParse(s, out retInt);

            return retInt;
        }

        [DebuggerStepThrough]
        public static long ToInt64(this string s) {
            long retInt;
            long.TryParse(s, out retInt);

            return retInt;
        }

        /// <summary>
        /// Generates an MD5 hash from the specified string
        /// </summary>
        /// <returns>MD5 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToMD5(this string str) {
            str.ThrowIfNull("str");

            return BitConverter.ToString(DigestHelper(new MD5Digest(), str)).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Generates a SHA1 hash from the specified string
        /// </summary>
        /// <returns>SHA1 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToSHA1(this string str) {
            str.ThrowIfNull("str");

            return BitConverter.ToString(DigestHelper(new Sha1Digest(), str)).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// Generates a SHA256 hash from the specified string
        /// </summary>
        /// <returns>SHA256 hash, uppercase.</returns>
        /// <param name="str">String.</param>
        public static string ToSHA256(this string str) {
            str.ThrowIfNull("str");

            return BitConverter.ToString(DigestHelper(new Sha256Digest(), str)).Replace("-", "").ToUpper();
        }

        [DebuggerStepThrough]
        public static float ToSingle(this string s) {
            float retInt;
            float.TryParse(s, out retInt);

            return retInt;
        }

        /// <summary>
        /// Truncate the specified string according to maxLength.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="maxLength">Max length.</param>
        /// <param name="append">Chars to append.</param>
        public static string Truncate(this string value, int maxLength, string append = "") {
            maxLength.ThrowIf(maxLength <= 0, "maxLength has to be positive.", "maxLength");

            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength - append.Length ? value : value.Substring(0, maxLength - append.Length) + append;
        }

        internal static char CharAt(this string s, int index) {
            s.ThrowIfNull("s");

            return index < s.Length ? s[index] : '\0';
        }

        internal static byte[] DigestHelper(GeneralDigest digest, string msg) {
            var msgBytes = Encoding.UTF8.GetBytes(msg);
            var bytes = new byte[digest.GetDigestSize()];

            digest.BlockUpdate(msgBytes, 0, msgBytes.Length);
            digest.DoFinal(bytes, 0);

            return bytes;
        }
    }
}