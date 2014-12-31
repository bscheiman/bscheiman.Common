#region
using System;
using System.Collections.Generic;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class ICollectionExtensionsTests {
        [Test]
        public void AddRange() {
            var list = new List<string>();
            List<string> nullList = null;

            Assert.Throws<ArgumentNullException>(() => nullList.AddRange("asd", "asdasd", "Asdasdasd"));
            Assert.Throws<ArgumentNullException>(() => list.AddRange(null));

            Assert.IsEmpty(list);
            Assert.DoesNotThrow(() => list.AddRange("asd", "asdasd", "Asdasdasd"));
            Assert.IsNotEmpty(list);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}