#region
using System;
using bscheiman.Common.Objects;
using bscheiman.Common.Util;
using NUnit.Framework;

#endregion

namespace bscheiman.Common.Tests {
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    [TestFixture]
    public class LoggingTests {
        [Test]
        public void Debug() {
            Assert.Throws<ArgumentNullException>(() => Log.Debug(null));
            Assert.DoesNotThrow(() => Log.Debug("LoggingTests.Debug"));
            Assert.DoesNotThrow(() => Log.Debug("LoggingTests.Debug Format {0}"));
            Assert.DoesNotThrow(() => Log.Debug("LoggingTests.Debug Format {0}", 1));
            Assert.Throws<ArgumentNullException>(() => Log.Info("LoggingTests.Debug Format {0}", null));
        }

        [Test]
        public void DoubleSetup() {
            Setup();
            Setup();
        }

        [Test]
        public void Error() {
            Assert.Throws<ArgumentNullException>(() => Log.Error(null));
            Assert.DoesNotThrow(() => Log.Error("LoggingTests.Error"));
            Assert.DoesNotThrow(() => Log.Error("LoggingTests.Error Format {0}"));
            Assert.DoesNotThrow(() => Log.Error("LoggingTests.Error Format {0}", 1));
            Assert.Throws<ArgumentNullException>(() => Log.Error("LoggingTests.Error Format {0}", null));
        }

        [Test]
        public void Fatal() {
            Assert.Throws<ArgumentNullException>(() => Log.Fatal((Exception) null));
            Assert.Throws<ArgumentNullException>(() => Log.Fatal((string) null));
            Assert.DoesNotThrow(() => Log.Fatal("LoggingTests.Fatal"));
            Assert.DoesNotThrow(() => Log.Fatal("LoggingTests.Fatal Format {0}"));
            Assert.DoesNotThrow(() => Log.Fatal("LoggingTests.Fatal Format {0}", new Exception()));
            Assert.DoesNotThrow(() => Log.Fatal(new Exception()));
            Assert.Throws<ArgumentNullException>(() => Log.Fatal("LoggingTests.Fatal Format {0}", null));
        }

        [Test]
        public void Info() {
            Assert.Throws<ArgumentNullException>(() => Log.Info(null));
            Assert.DoesNotThrow(() => Log.Info("LoggingTests.Info"));
            Assert.DoesNotThrow(() => Log.Info("LoggingTests.Info Format {0}"));
            Assert.DoesNotThrow(() => Log.Info("LoggingTests.Info Format {0}", 1));
            Assert.Throws<ArgumentNullException>(() => Log.Info("LoggingTests.Info Format {0}", null));
        }

        [TestFixtureSetUp]
        public void Setup() {
            Log.Setup(new LoggerParameters {
                LogEntriesToken = "c460cd9d-e8e4-43c3-a27e-d99239f2fa47"
            });

            Log.Setup();
        }

        [TestFixtureTearDown]
        public void TearDown() {
            Log.Teardown();
        }

        [Test]
        public void Warn() {
            Assert.Throws<ArgumentNullException>(() => Log.Warn(null));
            Assert.DoesNotThrow(() => Log.Warn("LoggingTests.Warn"));
            Assert.DoesNotThrow(() => Log.Warn("LoggingTests.Warn Format {0}"));
            Assert.DoesNotThrow(() => Log.Warn("LoggingTests.Warn Format {0}", 1));
            Assert.Throws<ArgumentNullException>(() => Log.Warn("LoggingTests.Warn Format {0}", null));
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}