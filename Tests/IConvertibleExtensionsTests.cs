#region
using System;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class IConvertibleExtensionsTests {
        [Test]
        public void To() {
            string n = null;

            Assert.Throws<ArgumentNullException>(() => n.To<int>());
            Assert.AreEqual(1, "1".To<int>());
            Assert.AreEqual(0, "---".To<int>());
            Assert.AreEqual(100, "---".To(100));
            Assert.AreEqual(1f, "1".To<float>());
            Assert.AreEqual(1.00, "1".To<double>());
            Assert.AreEqual(1M, "1".To<decimal>());
            Assert.AreEqual(0x01, "1".To<byte>());
            Assert.AreEqual('1', "1".To<char>());
            Assert.AreEqual(false, "False".To<bool>());
            Assert.AreEqual(true, "True".To<bool>());
            Assert.AreEqual(false, "1".To<bool>());
            Assert.AreEqual(false, "0".To<bool>());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}