#region
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class ByteExtensionsTests {
        [Test]
        public void GetString() {
            Assert.AreEqual("", "".FromHexString().GetString());
            Assert.AreEqual("hello", "68656c6c6f".FromHexString().GetString());
            Assert.AreEqual("hello", "efbbbf68656c6c6f".FromHexString().GetString());
            Assert.AreEqual("hello", "feff68656c6c6f".FromHexString().GetString());
            Assert.AreEqual("hello", "0000feff68656c6c6f".FromHexString().GetString());
            Assert.AreEqual("hello", "2b2f7668656c6c6f".FromHexString().GetString());
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
            Assert.AreEqual(digest.ToUpper(), msg.GetBytes().ToHMAC256(key.FromHexString()));
        }

        [Test, Sequential]
        public void ToHMAC256String([Values("Jefe")] string key,
            [Values("5bdcc146bf60754e6a042426089575c75a003f089d2739839dec58b964ec3843")] string digest,
            [Values("what do ya want for nothing?")] string msg) {
            Assert.AreEqual(digest.ToUpper(), msg.GetBytes().ToHMAC256(key));
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}