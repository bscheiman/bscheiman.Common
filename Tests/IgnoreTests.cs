#region
using System;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class IgnoreTests {
        [Test]
        public void Ignore() {
            Assert.DoesNotThrow(() => Helpers.Ignore.Exception(() => { throw new Exception(); }));

            Assert.AreEqual(Helpers.Ignore.Exception(() => { throw new Exception(); }, 2), 2);
            Assert.AreNotEqual(Helpers.Ignore.Exception(() => { throw new Exception(); }, 3), 2);
            Assert.AreEqual(Helpers.Ignore.Exception<int>(() => { throw new Exception(); }), default(int));
            Assert.AreEqual(Helpers.Ignore.Exception(() => 1), 1);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}