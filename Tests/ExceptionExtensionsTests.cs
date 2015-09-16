#region
using System;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class ExceptionExtensionsTests {
        [Test]
        public void Log() {
            Exception nullException = null;
            var ex = new Exception();

            Assert.Throws<ArgumentNullException>(() => nullException.Log());
            Assert.DoesNotThrow(() => ex.Log());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
