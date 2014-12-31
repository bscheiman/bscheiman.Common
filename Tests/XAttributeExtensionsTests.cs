#region
using System.Xml.Linq;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class XAttributeExtensionsTests {
        [Test]
        public void ValueOrDefault() {
            var attr = new XAttribute("attr", 123);
            var attrNullValue = new XAttribute("attr", string.Empty);
            XAttribute nullAttribute = null;

            Assert.AreEqual(string.Empty, nullAttribute.ValueOrDefault());
            Assert.AreEqual("asd", nullAttribute.ValueOrDefault("asd"));
            Assert.AreEqual(string.Empty, nullAttribute.ValueOrDefault());
            Assert.AreEqual("123", attr.ValueOrDefault());
            Assert.AreEqual(123, attr.ValueOrDefault<int>());
            Assert.AreEqual(123, attr.ValueOrDefault<long>());
            Assert.AreEqual(string.Empty, attrNullValue.ValueOrDefault());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}