#region
using System;
using bscheiman.Common.Extensions;
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    [TestFixture]
    public class DateTimeExtensionsTests {
        private readonly DateTime birth = new DateTime(1986, 6, 30);

        [Test]
        public void First() {
            Assert.AreEqual(new DateTime(1986, 6, 1), birth.First());
            Assert.AreEqual(new DateTime(1986, 6, 2), birth.First(DayOfWeek.Monday));
        }

        [Test]
        public void IsFuture() {
            Assert.IsTrue(DateUtil.NowDt.IsFuture(DateUtil.Epoch));
            Assert.IsFalse(birth.IsFuture());
        }

        [Test]
        public void IsPast() {
            Assert.IsFalse(DateUtil.NowDt.IsPast(DateUtil.Epoch));
            Assert.IsTrue(birth.IsPast());
            Assert.IsTrue(DateUtil.Epoch.IsPast());
            Assert.IsTrue(DateUtil.Epoch.IsPast(birth));
        }

        [Test]
        public void Last() {
            Assert.AreEqual(new DateTime(1986, 6, 30), birth.Last());
            Assert.AreEqual(new DateTime(1986, 6, 24), birth.Last(DayOfWeek.Tuesday));
        }

        [Test]
        public void Midnight() {
            Assert.AreEqual(new DateTime(1986, 6, 30, 0, 0, 0), birth.Midnight());
        }

        [Test]
        public void Next() {
        }

        [Test]
        public void Noon() {
            Assert.AreEqual(new DateTime(1986, 6, 30, 12, 0, 0), birth.Noon());
        }

        [Test]
        public void SetTime() {
            Assert.AreEqual(new DateTime(1986, 6, 30, 11, 23, 0), birth.SetTime(11, 23));
            Assert.AreEqual(new DateTime(1986, 6, 30, 11, 23, 1), birth.SetTime(11, 23, 1));
            Assert.AreEqual(new DateTime(1986, 6, 30, 11, 23, 1, 32), birth.SetTime(11, 23, 1, 32));
        }

        [Test]
        public void ToEpoch() {
            Assert.AreEqual(0L, DateUtil.Epoch.ToEpoch());
        }
    }
}