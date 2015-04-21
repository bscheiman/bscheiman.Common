#region
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using bscheiman.Common.Extensions;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

#endregion

namespace bscheiman.Common.Crypto {
    // Taken from https://gist.github.com/4336842
    public static class CryptoUtil {
        private const int Iterations = 10000;
        private const int KeyBitSize = 256;
        private const int MacBitSize = 128;
        private const int MinPasswordLength = 12;
        private const int NonceBitSize = 128;
        private const int SaltBitSize = 128;
        private static readonly SecureRandom Random = new SecureRandom();

        internal static byte[] Crypt(bool isEncrypt, string type, byte[] key, byte[] input) {
            var cipher = CipherUtilities.GetCipher(type);
            var cipherInfo = type.Split(new[] {
                '/'
            }, 3);

            if (cipher == null)
                return new byte[0];

            cipher.Init(isEncrypt, ParameterUtilities.CreateKeyParameter(cipherInfo[0], key));

            var bResult = new byte[cipher.GetOutputSize(input.Length)];
            var tam = cipher.ProcessBytes(input, 0, input.Length, bResult, 0);
            cipher.DoFinal(bResult, tam);

            return bResult;
        }

        public static byte[] Decrypt(string type, byte[] key, byte[] input) {
            return Crypt(false, type, key, input);
        }

        public static byte[] Decrypt(string type, byte[] key, byte[] iv, byte[] input) {
            return Crypt(false, type, key, input);
        }

        public static byte[] Encrypt(string type, byte[] key, byte[] input) {
            return Crypt(true, type, key, input);
        }

        public static byte[] Encrypt(string type, byte[] key, byte[] iv, byte[] input) {
            return Crypt(true, type, key, input);
        }

        internal static byte[] NewKey() {
            var key = new byte[KeyBitSize / 8];
            Random.NextBytes(key);

            return key;
        }

        public static string SimpleDecrypt(string encryptedMessage, byte[] key, int nonSecretPayloadLength = 0) {
            encryptedMessage.ThrowIfNullOrEmpty("encryptedMessage");
            nonSecretPayloadLength.ThrowIf(nonSecretPayloadLength < 0, "Invalid length", "nonSecretPayloadLength");

            var cipherText = Convert.FromBase64String(encryptedMessage);
            var plainText = SimpleDecrypt(cipherText, key, nonSecretPayloadLength);

            return plainText == null ? null : Encoding.UTF8.GetString(plainText, 0, plainText.Length);
        }

        public static byte[] SimpleDecrypt(byte[] encryptedMessage, byte[] key, int nonSecretPayloadLength = 0) {
            key.ThrowIfNull("key");
            key.ThrowIf(key.Length != KeyBitSize / 8, string.Format("Requires a {0}bit key", KeyBitSize), "key");
            encryptedMessage.ThrowIfNull("encryptedMessage");
            encryptedMessage.ThrowIf(encryptedMessage.Length == 0, "Message required", "encryptedMessage");

            using (var cipherStream = new MemoryStream(encryptedMessage))
            using (var cipherReader = new BinaryReader(cipherStream)) {
                var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
                var nonce = cipherReader.ReadBytes(NonceBitSize / 8);

                var cipher = new GcmBlockCipher(new AesFastEngine());
                var parameters = new AeadParameters(new KeyParameter(key), MacBitSize, nonce, nonSecretPayload);
                cipher.Init(false, parameters);

                var cipherText = cipherReader.ReadBytes(encryptedMessage.Length - nonSecretPayloadLength - nonce.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

                try {
                    var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                    cipher.DoFinal(plainText, len);
                } catch (InvalidCipherTextException) {
                    return null;
                }

                return plainText;
            }
        }

        public static string SimpleDecryptWithPassword(string encryptedMessage, string password, int nonSecretPayloadLength = 0) {
            encryptedMessage.ThrowIfNullOrEmpty("encryptedMessage");

            var cipherText = Convert.FromBase64String(encryptedMessage);
            var plainText = SimpleDecryptWithPassword(cipherText, password, nonSecretPayloadLength);

            return plainText == null ? null : Encoding.UTF8.GetString(plainText, 0, plainText.Length);
        }

        public static byte[] SimpleDecryptWithPassword(byte[] encryptedMessage, string password, int nonSecretPayloadLength = 0) {
            password.ThrowIfNullOrEmpty("password");
            password.ThrowIf(password.Length < MinPasswordLength, string.Format("Password must be {0}+ characters", MinPasswordLength), "password");
            encryptedMessage.ThrowIfNull("encryptedMessage");
            encryptedMessage.ThrowIf(encryptedMessage.Length == 0, "Message required", "encryptedMessage");

            var generator = new Pkcs5S2ParametersGenerator();

            var salt = new byte[SaltBitSize / 8];
            Array.Copy(encryptedMessage, nonSecretPayloadLength, salt, 0, salt.Length);

            generator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()), salt, Iterations);

            var key = (KeyParameter) generator.GenerateDerivedMacParameters(KeyBitSize);

            return SimpleDecrypt(encryptedMessage, key.GetKey(), salt.Length + nonSecretPayloadLength);
        }

        public static string SimpleEncrypt(string secretMessage, byte[] key, byte[] nonSecretPayload = null) {
            secretMessage.ThrowIfNullOrEmpty("secretMessage");

            var plainText = Encoding.UTF8.GetBytes(secretMessage);
            var cipherText = SimpleEncrypt(plainText, key, nonSecretPayload);

            return Convert.ToBase64String(cipherText);
        }

        public static byte[] SimpleEncrypt(byte[] secretMessage, byte[] key, byte[] nonSecretPayload = null) {
            key.ThrowIfNull("key");
            key.ThrowIf(key.Length != KeyBitSize / 8, string.Format("Requires a {0}bit key", KeyBitSize), "key");
            secretMessage.ThrowIfNull("secretMessage");
            secretMessage.ThrowIf(secretMessage.Length == 0, "Message required", "secretMessage");

            nonSecretPayload = nonSecretPayload ?? new byte[] {
            };

            var nonce = new byte[NonceBitSize / 8];
            Random.NextBytes(nonce, 0, nonce.Length);

            var cipher = new GcmBlockCipher(new AesFastEngine());
            var parameters = new AeadParameters(new KeyParameter(key), MacBitSize, nonce, nonSecretPayload);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(secretMessage.Length)];
            var len = cipher.ProcessBytes(secretMessage, 0, secretMessage.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            using (var combinedStream = new MemoryStream()) {
                using (var binaryWriter = new BinaryWriter(combinedStream)) {
                    binaryWriter.Write(nonSecretPayload);
                    binaryWriter.Write(nonce);
                    binaryWriter.Write(cipherText);
                }

                return combinedStream.ToArray();
            }
        }

        public static string SimpleEncryptWithPassword(string secretMessage, string password, byte[] nonSecretPayload = null) {
            secretMessage.ThrowIfNullOrEmpty("secretMessage");

            var plainText = Encoding.UTF8.GetBytes(secretMessage);
            var cipherText = SimpleEncryptWithPassword(plainText, password, nonSecretPayload);

            return Convert.ToBase64String(cipherText);
        }

        public static byte[] SimpleEncryptWithPassword(byte[] secretMessage, string password, byte[] nonSecretPayload = null) {
            nonSecretPayload = nonSecretPayload ?? new byte[] {
            };

            password.ThrowIfNullOrEmpty("password");
            password.ThrowIf(password.Length < MinPasswordLength, string.Format("Password must be {0}+ characters", MinPasswordLength), "password");
            secretMessage.ThrowIfNull("secretMessage");
            secretMessage.ThrowIf(secretMessage.Length == 0, "Message required", "secretMessage");

            var generator = new Pkcs5S2ParametersGenerator();

            var salt = new byte[SaltBitSize / 8];
            Random.NextBytes(salt);

            generator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()), salt, Iterations);

            var key = (KeyParameter) generator.GenerateDerivedMacParameters(KeyBitSize);

            var payload = new byte[salt.Length + nonSecretPayload.Length];
            Array.Copy(nonSecretPayload, payload, nonSecretPayload.Length);
            Array.Copy(salt, 0, payload, nonSecretPayload.Length, salt.Length);

            return SimpleEncrypt(secretMessage, key.GetKey(), payload);
        }
    }
}