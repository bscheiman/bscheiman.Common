#region
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class LongExtensionsTests {
        [Test]
        public void FromEpoch() {
            Assert.AreEqual(DateUtil.Now, 0);
        }

        [Test]
        public void ToGb() {
        }

        [Test]
        public void ToKb() {
        }

        [Test]
        public void ToMb() {
        }

        [Test]
        public void ToTb() {
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}