#region
using System;
using bscheiman.Common.Helpers;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class RandomExtensionTests {
        internal static Random RandGen = RandomHelper.Instance;

        [Test]
        public void Method() {
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}