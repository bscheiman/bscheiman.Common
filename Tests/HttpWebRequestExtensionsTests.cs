#region
using System.Net;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class HttpWebRequestExtensionsTests {
        [Test]
        public void SetRawHeader() {
            var request = (HttpWebRequest) WebRequest.Create("http://www.google.com");

            Assert.IsNullOrEmpty(request.Headers["Test"]);
            Assert.IsNullOrEmpty(request.Headers["Referer"]);

            request.SetRawHeader("Test", "123");
            request.SetRawHeader("Referer", "www");
            Assert.AreEqual("123", request.Headers["Test"]);
            Assert.AreEqual("www", request.Headers["Referer"]);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}
