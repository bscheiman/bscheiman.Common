#region
using System.ComponentModel;
using bscheiman.Common.Extensions;
using NUnit.Framework;
using Description = System.ComponentModel.DescriptionAttribute;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class EnumExtensionTests {
        public enum Test {
            None,
            [Description("Lalala")] Description
        }

        [Test]
        public void GetAttributeOfType() {
            var desc = Test.Description.GetAttributeOfType<Description>();
            var noDesc = Test.None.GetAttributeOfType<Description>();

            Assert.IsNull(noDesc);
            Assert.IsNotNull(desc);
            Assert.IsNotEmpty(desc.Description);

            Assert.AreEqual(desc.Description, Test.Description.GetDescription());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}