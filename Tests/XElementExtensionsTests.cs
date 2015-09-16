#region
using System;
using System.Xml.Linq;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class XElementExtensionsTests {
        [Test]
        public void ValueOrDefault() {
            var element = new XElement("test", new XAttribute("attr", 123), "456");
            var elementNullValue = new XElement("test", new XAttribute("attr", 123));
            XElement nullElement = null;

            Assert.Throws<ArgumentNullException>(() => nullElement.ValueOrDefault());
            Assert.Throws<ArgumentNullException>(() => nullElement.ValueOrDefault("asd"));
            Assert.Throws<ArgumentNullException>(() => nullElement.ValueOrDefault(null));

            Assert.AreEqual("456", element.ValueOrDefault());
            Assert.AreEqual(456, element.ValueOrDefault<int>());
            Assert.AreEqual(456, element.ValueOrDefault<long>());
            Assert.AreEqual(string.Empty, elementNullValue.ValueOrDefault());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
