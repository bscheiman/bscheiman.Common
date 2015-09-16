#region
using System;
using bscheiman.Common.Extensions;
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
        public void NextBool() {
            Assert.DoesNotThrow(() => RandGen.NextBool());
            Assert.DoesNotThrow(() => RandGen.NextBool(0.75));
        }

        [Test]
        public void NextChar() {
            Assert.IsTrue(RandGen.NextChar(CharType.AlphabeticLower).Between('a', 'z'));
            Assert.IsTrue(RandGen.NextChar(CharType.AlphabeticUpper).Between('A', 'Z'));
            Assert.IsTrue(RandGen.NextChar(CharType.AlphabeticAny).Between('A', 'z'));
            Assert.IsTrue(RandGen.NextChar(CharType.Numeric).Between('0', '9'));

            char alphaNumLower = RandGen.NextChar(CharType.AlphanumericLower);
            char alphaNumUpper = RandGen.NextChar(CharType.AlphanumericUpper);
            char alphaNumAny = RandGen.NextChar();

            Assert.IsTrue(alphaNumLower.Between('a', 'z') || alphaNumLower.Between('0', '9'));
            Assert.IsTrue(alphaNumUpper.Between('A', 'Z') || alphaNumUpper.Between('0', '9'));
            Assert.IsTrue(alphaNumAny.Between('A', 'z') || alphaNumAny.Between('0', '9'));
        }

        [Test]
        public void NextDateTime() {
            Assert.AreNotEqual(RandGen.NextDateTime(), RandGen.NextDateTime());
            Assert.AreNotEqual(RandGen.NextDateTime(), RandGen.NextDateTime());
            Assert.AreNotEqual(RandGen.NextDateTime(), RandGen.NextDateTime());
            Assert.AreNotEqual(RandGen.NextDateTime(), RandGen.NextDateTime());
            Assert.AreNotEqual(RandGen.NextDateTime(), RandGen.NextDateTime());
        }

        [Test]
        public void NextDouble() {
            Assert.Throws<ArgumentException>(() => RandGen.NextDouble(100, 0));

            Assert.AreNotEqual(0, RandGen.NextDouble(double.NegativeInfinity, double.PositiveInfinity));
            Assert.AreNotEqual(RandGen.NextDouble(), RandGen.NextDouble());
            Assert.AreNotEqual(RandGen.NextDouble(), RandGen.NextDouble());
            Assert.AreNotEqual(RandGen.NextDouble(), RandGen.NextDouble());
            Assert.AreNotEqual(RandGen.NextDouble(), RandGen.NextDouble());
            Assert.AreNotEqual(RandGen.NextDouble(), RandGen.NextDouble());
        }

        [Test]
        public void NextString() {
            Assert.AreNotEqual(RandGen.NextString(12), RandGen.NextString(12));
            Assert.AreNotEqual(RandGen.NextString(12), RandGen.NextString(12));
            Assert.AreNotEqual(RandGen.NextString(12), RandGen.NextString(12));
            Assert.AreNotEqual(RandGen.NextString(12), RandGen.NextString(12));
            Assert.AreNotEqual(RandGen.NextString(12), RandGen.NextString(12));
        }

        [Test]
        public void NextTimeSpan() {
            Assert.AreNotEqual(RandGen.NextTimeSpan(), RandGen.NextTimeSpan());
            Assert.AreNotEqual(RandGen.NextTimeSpan(), RandGen.NextTimeSpan());
            Assert.AreNotEqual(RandGen.NextTimeSpan(), RandGen.NextTimeSpan());
            Assert.AreNotEqual(RandGen.NextTimeSpan(), RandGen.NextTimeSpan());
            Assert.AreNotEqual(RandGen.NextTimeSpan(), RandGen.NextTimeSpan());
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
