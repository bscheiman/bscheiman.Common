#region
using System;
using bscheiman.Common.Extensions;
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class LongExtensionsTests {
        internal static long Zero = 0L;

        [Test]
        public void FromEpoch() {
            Assert.AreEqual(DateUtil.Epoch, Zero.FromEpoch());
            Assert.AreEqual(DateUtil.Epoch.ToEpoch(), Zero.FromEpoch().ToEpoch());

            Assert.AreEqual(DateTime.SpecifyKind(new DateTime(2014, 12, 31, 19, 0, 0, 0), DateTimeKind.Utc), 1420052400.FromEpoch());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
