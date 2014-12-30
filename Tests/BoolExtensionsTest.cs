#region
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class BoolExtensionsTest {
        [Test]
        public void WhenFalse() {
            Assert.AreEqual(false.WhenFalse("this is false"), "this is false");
            Assert.AreEqual(false.WhenFalse(() => "this is false"), "this is false");
            Assert.AreEqual(true.WhenFalse("this is false"), null);
            Assert.AreEqual(true.WhenFalse(() => "this is false"), null);
        }

        [Test]
        public void WhenTrue() {
            Assert.AreEqual(true.WhenTrue("this is true"), "this is true");
            Assert.AreEqual(true.WhenTrue(() => "this is true"), "this is true");
            Assert.AreEqual(false.WhenTrue("this is true"), null);
            Assert.AreEqual(false.WhenTrue(() => "this is true"), null);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}