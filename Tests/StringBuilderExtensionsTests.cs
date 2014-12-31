#region
using System;
using System.Text;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class StringBuilderExtensionsTests {
        [Test]
        public void AppendLine() {
            var sb = new StringBuilder();
            StringBuilder nullSb = null;

            Assert.Throws<ArgumentNullException>(() => nullSb.AppendLine("", ""));
            Assert.Throws<ArgumentNullException>(() => sb.AppendLine(null, ""));
            Assert.Throws<ArgumentNullException>(() => sb.AppendLine(null, null));

            sb.AppendLine("Testing {0}", 123);

            Assert.AreEqual("Testing 123\r\n", sb.ToString());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}