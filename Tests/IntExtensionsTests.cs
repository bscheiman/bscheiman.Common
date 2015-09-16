#region
using System;
using System.Linq;
using bscheiman.Common.Extensions;
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class IntExtensionsTests {
        internal static int Zero = 0;

        [Test]
        public void FromEpoch() {
            Assert.AreEqual(DateUtil.Epoch, Zero.FromEpoch());
            Assert.AreEqual(DateUtil.Epoch.ToEpoch(), Zero.FromEpoch().ToEpoch());

            Assert.AreEqual(DateTime.SpecifyKind(new DateTime(2014, 12, 31, 19, 0, 0, 0), DateTimeKind.Utc), 1420052400.FromEpoch());
        }

        [Test]
        public void Range() {
            Assert.IsEmpty(Zero.Range(-2));

            Assert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Zero.Range(5).ToArray());
        }

        [Test]
        public void RangeInclusive() {
            Assert.IsEmpty(Zero.RangeInclusive(-2));

            Assert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, Zero.RangeInclusive(5).ToArray());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
