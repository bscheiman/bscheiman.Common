#region
using System;
using System.Collections.Generic;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    [TestFixture]
    public class DictionaryExtensionsTests {
        private static readonly Dictionary<string, int> TestDictionary = new Dictionary<string, int>();

        [Test]
        public void Get() {
            TestDictionary.Add("blah", 1);

            Dictionary<string, int> nullDictionary = null;

            Assert.Throws<ArgumentNullException>(() => nullDictionary.Get("blah"));
            Assert.AreEqual(1, TestDictionary.Get("blah"));
            Assert.AreEqual(default(int), TestDictionary.Get("bleh"));

            TestDictionary["blah"] = Int32.MaxValue;

            Assert.AreEqual(Int32.MaxValue, TestDictionary.Get("blah"));
        }
    }
}