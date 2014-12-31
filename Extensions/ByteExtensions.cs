#region
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace bscheiman.Common.Extensions {
    public static class ByteExtensions {
        public static string GetString(this byte[] buffer) {
            return buffer.GetString(null);
        }

        public static string GetString(this byte[] buffer, Encoding encoding) {
            if (buffer == null || buffer.Length == 0)
                return "";

            int offset = 0;

            if (encoding == null) {
                if (buffer.Length >= 3 && buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf) {
                    encoding = Encoding.UTF8;
                    offset = 3;
                } else if (buffer.Length >= 2 && buffer[0] == 0xff && buffer[1] == 0xfe) {
                    encoding = Encoding.Unicode;
                    offset = 2;
                } else if (buffer.Length >= 2 && buffer[0] == 0xfe && buffer[1] == 0xff) {
                    encoding = Encoding.BigEndianUnicode;
                    offset = 2;
                } else if (buffer.Length >= 4 && buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff) {
                    encoding = Encoding.UTF32;
                    offset = 4;
                } else if (buffer.Length >= 3 && buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76) {
                    encoding = Encoding.UTF7;
                    offset = 3;
                }
            }

            using (var stream = new MemoryStream()) {
                stream.Write(buffer, offset, buffer.Length - offset);
                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream, encoding ?? Encoding.Default))
                    return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Converts a byte array to its hex string representation.
        /// </summary>
        /// <param name="byteArray">Byte array</param>
        /// <param name="upperCase">...</param>
        /// <param name="splitInGroups"></param>
        /// <param name="separator"></param>
        /// <returns>A hex string</returns>
        public static string ToHexString(this byte[] byteArray, bool upperCase = false, int splitInGroups = -1, string separator = " ") {
            string str = BitConverter.ToString(byteArray).Replace("-", "");

            str = upperCase ? str.ToUpper() : str.ToLower();

            if (splitInGroups > 0)
                str = str.SplitInParts(splitInGroups).Join(separator);

            return str;
        }

        public static string ToHMAC256(this byte[] bytes, string key) {
            return bytes.ToHMAC256(Encoding.UTF8.GetBytes(key));
        }

        public static string ToHMAC256(this byte[] bytes, byte[] key) {
            using (var hmac = new HMACSHA256(key))
                return BitConverter.ToString(hmac.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }
    }
}