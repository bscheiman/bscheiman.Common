#region
using System.Threading;
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class DateUtilTests {
        [Test]
        public void EpochDoesntChange() {
            var epoch = DateUtil.Epoch;
            Thread.Sleep(1500);

            Assert.AreEqual(epoch, DateUtil.Epoch);
        }

        [Test]
        public void NowChanges() {
            long now = DateUtil.Now;
            var nowDt = DateUtil.NowDt;

            Thread.Sleep(1500);

            Assert.AreNotEqual(now, DateUtil.Now);
            Assert.AreNotEqual(nowDt, DateUtil.NowDt);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}