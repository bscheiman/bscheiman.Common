#region
using bscheiman.Common.Attributes;
using bscheiman.Common.Extensions;
using NUnit.Framework;
using Description = bscheiman.Common.Attributes.DescriptionAttribute;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class EnumExtensionTests {
        public enum Test {
            NoDesc,
            [Description("Lalala")] HasDesc
        }

        [Test]
        public void GetAttributeOfType() {
            var desc = Test.HasDesc.GetAttributeOfType<Description>();
            var noDesc = Test.NoDesc.GetAttributeOfType<Description>();

            Assert.IsNull(noDesc);
            Assert.IsNotNull(desc);
            Assert.IsNotEmpty(desc.Description);

            Assert.AreEqual(desc.Description, Test.HasDesc.GetDescription());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}