#region
using System;
using System.Globalization;
using System.Text;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    [TestFixture]
    public class StringExtensionsTests {
        internal static string NullString = null;

        [Test]
        public void AsEnum() {
            Assert.Throws<ArgumentException>(() => "asdasd".AsEnum<Importance>());
            Assert.Throws<ArgumentException>(() => "asdasd".AsEnum<string>());
            Assert.Throws<ArgumentNullException>(() => NullString.AsEnum<Importance>());

            Assert.AreEqual(Importance.None, "None".AsEnum<Importance>());
            Assert.AreEqual(Importance.Trivial, "trivial".AsEnum<Importance>());
            Assert.AreEqual(Importance.Regular, "reGuLar".AsEnum<Importance>());
            Assert.AreEqual(Importance.Important, "Important".AsEnum<Importance>());
            Assert.AreEqual(Importance.Critical, "Critical".AsEnum<Importance>());
        }

        [Test]
        public void CIContains() {
            Assert.Throws<ArgumentException>(() => NullString.CIContains("asd"));
            Assert.Throws<ArgumentException>(() => "asd".CIContains(NullString));

            Assert.IsTrue("the quick brown fox".CIContains("BROWN"));
            Assert.IsFalse("the quick brown fox".CIContains("cat"));
        }

        [Test]
        public void CIEndsWith() {
            Assert.Throws<ArgumentException>(() => NullString.CIEndsWith("asd"));
            Assert.Throws<ArgumentException>(() => "asd".CIEndsWith(NullString));

            Assert.IsTrue("the quick brown fox".CIEndsWith("FOX"));
            Assert.IsFalse("the quick brown fox".CIEndsWith("cat"));
        }

        [Test]
        public void CIIndexOf() {
            Assert.Throws<ArgumentException>(() => NullString.CIIndexOf("asd"));
            Assert.Throws<ArgumentException>(() => "asd".CIIndexOf(NullString));

            Assert.IsTrue("the quick brown fox".CIIndexOf("q") >= 0);
            Assert.IsFalse("the quick brown fox".CIIndexOf("z") >= 0);
        }

        [Test]
        public void CIStartsWith() {
            Assert.Throws<ArgumentException>(() => NullString.CIStartsWith("asd"));
            Assert.Throws<ArgumentException>(() => "asd".CIStartsWith(NullString));

            Assert.IsTrue("the quick brown fox".CIStartsWith("THE"));
            Assert.IsFalse("the quick brown fox".CIStartsWith("cat"));
        }

        [Test]
        public void FormatWith() {
            Assert.AreEqual("Boris Scheiman", "{0} {1}".FormatWith("Boris", "Scheiman"));
        }

        [Test]
        public void FromJson() {
            string json = @"{  ""firstName"":""John"", ""lastName"":""Doe"" }";
            var obj = json.FromJson<Person>();

            Assert.AreEqual("John", obj.FirstName);
            Assert.AreEqual("Doe", obj.LastName);
            Assert.IsNotNull(obj);
        }

        [Test]
        public void GetBytes([Values("lalala", "ñasdasd", "123123", "")] string str) {
            foreach (var encoding in new[] {
                Encoding.UTF8, Encoding.Unicode
            }) {
                Assert.Throws<ArgumentNullException>(() => NullString.GetBytes());
                Assert.Throws<ArgumentNullException>(() => NullString.GetBytes(encoding));
                Assert.Throws<ArgumentNullException>(() => NullString.GetBytes(null));

                Assert.AreEqual(encoding.GetBytes(str), str.GetBytes(encoding));
            }
        }

        [Test]
        public void IsLike() {
            Assert.Throws<ArgumentNullException>(() => NullString.IsLike(""));
            Assert.Throws<ArgumentNullException>(() => "".IsLike(NullString));
            Assert.Throws<ArgumentException>(() => "".IsLike(@"\X\z"));

            Assert.IsTrue("b55scheiman".IsLike(@"b55.*?"));
            Assert.IsFalse("b55scheiman".IsLike(@"b7.*?"));
        }

        [Test]
        public void IsNullOrEmpty() {
            Assert.IsTrue(NullString.IsNull());
            Assert.IsTrue(NullString.IsNullOrEmpty());
            Assert.IsTrue("".IsNullOrEmpty());

            Assert.IsFalse("".IsNull());
            Assert.IsFalse("asdf".IsNull());
            Assert.IsFalse("asdf".IsNullOrEmpty());
        }

        [Test]
        public void MatchesWildcard() {
            string str = "the quick brown fox jumps over the lazy dog's back";

            Assert.Throws<ArgumentNullException>(() => NullString.MatchesWildcard("*cat*"));

            Assert.IsFalse(str.MatchesWildcard("*cat*"));
            Assert.IsFalse(str.MatchesWildcard("dog"));
            Assert.IsTrue(str.MatchesWildcard("*dog*"));
        }

        [Test]
        public void SplitRe() {
            Assert.Throws<ArgumentNullException>(() => NullString.SplitRe(""));
            Assert.Throws<ArgumentNullException>(() => "".SplitRe(null));

            Assert.AreEqual(new[] {
                "1", "2", "3"
            }, "1yay2yay3".SplitRe("yay"));
        }

        [Test]
        public void SplitRemoveEmptyEntries() {
            string str = "hey\r\nhi\r\n\r\n\r\nhowdy";
            string str2 = "hey-hi---howdy";

            Assert.Throws<ArgumentNullException>(() => ((string) null).SplitRemoveEmptyEntries());
            Assert.Throws<ArgumentNullException>(() => "".SplitRemoveEmptyEntries(null));

            Assert.AreEqual(new[] {
                "hey", "hi", "howdy"
            }, str.SplitRemoveEmptyEntries());
            Assert.AreEqual(new[] {
                "hey", "hi", "howdy"
            }, str2.SplitRemoveEmptyEntries('-'));
        }

        [Test]
        public void ToBool() {
            Assert.IsTrue("1".ToBool());
            Assert.IsTrue("true".ToBool());
            Assert.IsTrue("True".ToBool());
            Assert.IsTrue("TRUE".ToBool());
            Assert.IsTrue("Y".ToBool());
            Assert.IsTrue("T".ToBool());

            Assert.IsFalse("".ToBool());
            Assert.IsFalse("0".ToBool());
            Assert.IsFalse("false".ToBool());
            Assert.IsFalse("False".ToBool());
            Assert.IsFalse("FALSE".ToBool());
            Assert.IsFalse("N".ToBool());
            Assert.IsFalse("F".ToBool());
        }

        [Test]
        public void ToDecimal() {
            Assert.AreEqual(0M, "0".ToDecimal());
            Assert.AreEqual(0M, "-".ToDecimal());
            Assert.AreEqual(0M, "".ToDecimal());
            Assert.AreEqual(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ToDecimal());
            Assert.AreEqual(decimal.MinValue, decimal.MinValue.ToString(CultureInfo.InvariantCulture).ToDecimal());
        }

        [Test]
        public void ToDouble() {
            Assert.AreEqual(0d, "0".ToDouble());
            Assert.AreEqual(0d, "-".ToDouble());
            Assert.AreEqual(0d, "".ToDouble());
            Assert.AreEqual(30495d, "30495".ToDouble());
            Assert.AreEqual(-30495d, "-30495".ToDouble());
        }

        [Test]
        public void ToHexString() {
            Assert.Throws<ArgumentNullException>(() => NullString.FromHexString().ToHexString(true, 4));
            Assert.Throws<FormatException>(() => "ABCDEFGHUHKKJNSADFADFZX".FromHexString().ToHexString(true, 4));

            Assert.AreEqual("1234 5678 9ABC", "123456789aBC".FromHexString().ToHexString(true, 4));
            Assert.AreEqual("1234:5678:9ABC", "123456789aBC".FromHexString().ToHexString(true, 4, ":"));
            Assert.AreEqual("123:456:789:ABC", "123456789aBC".FromHexString().ToHexString(true, 3, ":"));
        }

        [Test, Sequential]
        public void ToHMAC256Bytes(
            [Values("0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b0b", "4a656665", "0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c0c",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                )] string key,
            [Values("b0344c61d8db38535ca8afceaf0bf12b881dc200c9833da726e9376c2e32cff7",
                "5bdcc146bf60754e6a042426089575c75a003f089d2739839dec58b964ec3843",
                "a3b6167473100ee06e0c796c2955552bfa6f7c0a6a8aef8b93f860aab0cd20c5",
                "60e431591ee0b67f0d8a26aacbf5b77f8e0bc6213728c5140546040f0ee37f54",
                "9b09ffa71b942fcb27635fbcd5b0e944bfdc63644f0713938a7f51535c3a35e2")] string digest,
            [Values("Hi There", "what do ya want for nothing?", "Test With Truncation",
                "Test Using Larger Than Block-Size Key - Hash Key First",
                "This is a test using a larger than block-size key and a larger than block-size data. The key needs to be hashed before being used by the HMAC algorithm."
                )] string msg) {
            Assert.AreEqual(digest.ToUpper(), msg.ToHMAC256(key.FromHexString()));
        }

        [Test, Sequential]
        public void ToHMAC256String([Values("Jefe")] string key,
                                    [Values("5bdcc146bf60754e6a042426089575c75a003f089d2739839dec58b964ec3843")] string digest,
                                    [Values("what do ya want for nothing?")] string msg) {
            Assert.AreEqual(digest.ToUpper(), msg.ToHMAC256(key));
        }

        [Test]
        public void ToInt32() {
            Assert.AreEqual(0, "0".ToInt32());
            Assert.AreEqual(0, "-".ToInt32());
            Assert.AreEqual(0, "".ToInt32());
            Assert.AreEqual(Int32.MaxValue, Int32.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt32());
            Assert.AreEqual(Int32.MinValue, Int32.MinValue.ToString(CultureInfo.InvariantCulture).ToInt32());
        }

        [Test]
        public void ToInt64() {
            Assert.AreEqual(0, "0".ToInt64());
            Assert.AreEqual(0, "-".ToInt64());
            Assert.AreEqual(0, "".ToInt64());
            Assert.AreEqual(Int64.MaxValue, Int64.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt64());
            Assert.AreEqual(Int64.MinValue, Int64.MinValue.ToString(CultureInfo.InvariantCulture).ToInt64());
        }

        [Test, Sequential]
        public void ToMD5(
            [Values("d41d8cd98f00b204e9800998ecf8427e", "0cc175b9c0f1b6a831c399e269772661", "900150983cd24fb0d6963f7d28e17f72",
                "f96b697d7cb7938d525a2f31aaf161d0", "c3fcd3d76192e4007dfb496cca67e13b", "d174ab98d277d9f5a5611c2c9f419d9f",
                "57edf4a22be3c955ac49da2e2107b67a")] string digest,
            [Values("", "a", "abc", "message digest", "abcdefghijklmnopqrstuvwxyz",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890")] string source) {
            Assert.AreEqual(digest.ToUpper(), source.ToMD5());
        }

        [Test, Sequential]
        public void ToSHA1(
            [Values("a9993e364706816aba3e25717850c26c9cd0d89d", "da39a3ee5e6b4b0d3255bfef95601890afd80709",
                "84983e441c3bd26ebaae4aa1f95129e5e54670f1", "a49b2446a02c645bf419f995b67091253a04a259")] string digest,
            [Values("abc", "", "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq",
                "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu")] string
                source) {
            Assert.AreEqual(digest.ToUpper(), source.ToSHA1());
        }

        [Test, Sequential]
        public void ToSHA256(
            [Values("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad",
                "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                "248d6a61d20638b8e5c026930c3e6039a33ce45964ff2167f6ecedd419db06c1",
                "cf5b16a778af8380036ce59e7b0492370b249b11e8f07a51afac45037afee9d1")] string digest,
            [Values("abc", "", "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq",
                "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu")] string
                source) {
            Assert.AreEqual(digest.ToUpper(), source.ToSHA256());
        }

        [Test]
        public void ToSingle() {
            Assert.AreEqual(0f, "0".ToSingle());
            Assert.AreEqual(0f, "-".ToSingle());
            Assert.AreEqual(0f, "".ToSingle());
            Assert.AreEqual(30495f, "30495".ToSingle());
            Assert.AreEqual(-30495f, "-30495".ToSingle());
        }

        [Test]
        public void Truncate() {
            Assert.AreEqual("12345", "1234567890".Truncate(5));
            Assert.AreEqual("12...", "1234567890".Truncate(5, "..."));
            Assert.AreEqual("12...", "12345".Truncate(5, "..."));
            Assert.AreEqual("", "".Truncate(5, "..."));
        }

        private enum Importance {
            None,
            Trivial,
            Regular,
            Important,
            Critical
        };

        private class Person {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}