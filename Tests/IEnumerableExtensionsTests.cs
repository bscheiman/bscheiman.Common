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
    public class IEnumerableExtensionsTests {
        private readonly string[] str = { "foo", "bar", "fizz", "buzz" };

        [Test]
        public void EmptyIfNull() {
            List<string> list = null;

            Assert.IsEmpty(list.EmptyIfNull());
        }

        [Test]
        public void ForEach() {
            int i = 0;
            str.ForEach(s => i++);

            Assert.IsTrue(i == 4);
            Assert.Throws<ArgumentNullException>(() => new string[0].ForEach(null));
            Assert.DoesNotThrow(() => new string[0].ForEach(s => { }));
            Assert.DoesNotThrow(() => ((string[]) null).ForEach(s => { }));
        }

        [Test]
        public void GetRandomElement() {
            Assert.Throws<ArgumentNullException>(() => ((string[]) null).GetRandomElement());
            Assert.IsNotEmpty(str.GetRandomElement());
            Assert.DoesNotThrow(() => new string[0].GetRandomElement());
        }

        [Test]
        public void IsNullOrEmpty() {
            Assert.IsTrue(((string[]) null).IsNullOrEmpty());
            Assert.IsFalse(str.IsNullOrEmpty());
            Assert.IsTrue(new string[0].IsNullOrEmpty());
        }

        [Test]
        public void Join1() {
            var blah = new[] { new {
                Name = "asd"
            },
                new {
                    Name = "bleh"
                } };

            Assert.AreEqual("asd-bleh", blah.Join(p => p.Name, "-"));
        }

        [Test]
        public void Join2() {
            Assert.AreEqual("", new string[0].Join());
            Assert.AreEqual("", new string[0].Join(","));
            Assert.AreEqual("", ((string[]) null).Join());

            Assert.AreEqual("foo-bar-fizz-buzz", str.Join("-"));
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
