#region
using System;
using System.Threading;
using bscheiman.Common.Extensions;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class EventHandlerExtensionsTests {
        [Test]
        public void InvokeAsyncArgs() {
            Assert.DoesNotThrow((() => ((EventHandler<EventArgs>) null).InvokeAsync(this, null)));

            var mre = new ManualResetEvent(false);
            Args += (sender, args) => mre.Set();
            Args.InvokeAsync(this, null);

            mre.WaitOne();
            Console.WriteLine("Event received");
        }

        [Test]
        public void InvokeAsyncNoArgs() {
            Assert.DoesNotThrow((() => ((EventHandler) null).InvokeAsync(this, null)));

            var mre = new ManualResetEvent(false);
            NoArgs += (sender, args) => mre.Set();
            NoArgs.InvokeAsync(this, null);

            mre.WaitOne();
            Console.WriteLine("Event received");
        }

        private event EventHandler<EventArgs> Args = delegate { };
        private event EventHandler NoArgs = delegate { };
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}