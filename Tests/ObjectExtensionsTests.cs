#region
using System;
using System.Collections.Generic;
using System.Linq;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class ObjectExtensionsTests {
        [Test]
        public void As() {
            Assert.IsNotNull("".As<string>());
            Assert.IsNull("".As<DescriptionAttribute>());
        }

        [Test]
        public void Between() {
            Assert.IsTrue(1.Between(0, 100));
            Assert.IsTrue(1.Between(1, 1));
            Assert.IsTrue(5.Between(4, 5));
            Assert.IsTrue('c'.Between('a', 'd'));
            Assert.IsFalse(0.Between(1, 4));
            Assert.IsFalse(6.Between(5, 4));

            Assert.IsTrue(9f.Between(0f, 10f));
            Assert.IsTrue(9M.Between(0M, 10M));
            Assert.IsTrue("Hi".Between("Az", "Zo"));
        }

        [Test]
        public void GetMemberName() {
            var desc = new DescriptionAttribute("Test");

            Assert.AreEqual(desc.GetMemberName(d => d.Description), "Description");
        }

        [Test]
        public void Is() {
            Assert.IsTrue("string".Is<string>());
            Assert.IsTrue("string".Is<object>());
            Assert.IsFalse("string".Is<DescriptionAttribute>());
            Assert.IsFalse("string".Is<List>());
        }

        [Test]
        public void IsIn() {
            var list = 1.Range(10).ToArray();

            Assert.IsTrue(1.IsIn(list));
            Assert.IsTrue(2.IsIn(list));
            Assert.IsTrue(10.IsIn(list));
            Assert.IsFalse(11.IsIn(list));
        }

        [Test]
        public void IsNot() {
            Assert.IsFalse("string".IsNot<string>());
            Assert.IsFalse("string".IsNot<object>());
            Assert.IsTrue("string".IsNot<DescriptionAttribute>());
            Assert.IsTrue("string".IsNot<List>());
        }

        [Test]
        public void IsNotIn() {
            var list = 1.Range(10).ToArray();

            Assert.IsFalse(1.IsNotIn(list));
            Assert.IsFalse(2.IsNotIn(list));
            Assert.IsFalse(10.IsNotIn(list));
            Assert.IsTrue(11.IsNotIn(list));
        }

        [Test]
        public void IsNull() {
            string str = null;

            Assert.IsTrue(str.IsNull());
            Assert.IsFalse("".IsNull());
        }

        [Test]
        public void NullOr() {
            string str = null;

            Assert.AreEqual("str", str.NullOr(s => "str2", "str"));
            Assert.AreEqual("str2", "str2".NullOr(s => "str2", "str"));
        }

        [Test]
        public void Safe() {
            var list = new List<string>();
            List<string> nullList = null;

            Assert.AreEqual(list.Safe(), new List<string>());
            Assert.AreEqual(nullList.Safe(), new List<string>());
        }

        [Test]
        public void ThrowIf() {
            Assert.Throws<ArgumentException>(() => { 0.ThrowIf(true, "Throws", "1"); });
            Assert.DoesNotThrow(() => { 0.ThrowIf(false, "Doesn't throw", "1"); });
        }

        [Test]
        public void ThrowIfNull() {
            string str = null;

            Assert.Throws<ArgumentNullException>(() => { str.ThrowIfNull("str"); });
            Assert.DoesNotThrow(() => { "asd".ThrowIfNull("str"); });
        }

        [Test]
        public void ToJson() {
            Assert.AreEqual(@"{""Name"":""name""}", new Dummy {
                Name = "name"
            }.ToJson());
        }

        [Test]
        public void With() {
            var dummy = new Dummy();

            Assert.IsNullOrEmpty(dummy.Name);
            dummy.With(d => d.Name = "asd");
            Assert.AreEqual("asd", dummy.Name);
        }

        public class Dummy {
            public string Name { get; set; }
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}