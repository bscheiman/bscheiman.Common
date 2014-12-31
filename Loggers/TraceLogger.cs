#region
using System.Diagnostics;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    internal class TraceLogger : ILogger {
        public bool CanUse(LoggerParameters parms) {
            return true;
        }

        public void Debug(string str) {
            Trace.TraceInformation(str);
        }

        public void Error(string str) {
            Trace.TraceError(str);
        }

        public void Fatal(string str) {
            Trace.TraceError(str);
        }

        public void Info(string str) {
            Trace.TraceInformation(str);
        }

        public void Setup(LoggerParameters parms) {
        }

        public void Teardown() {
        }

        public void Warn(string str) {
            Trace.TraceWarning(str);
        }
    }
}