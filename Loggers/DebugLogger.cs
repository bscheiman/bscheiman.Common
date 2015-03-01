#region
using bscheiman.Common.Objects;
using DiagDebug = System.Diagnostics.Debug;

#endregion

namespace bscheiman.Common.Loggers {
    internal class DebugLogger : ILogger {
        public bool CanUse(LoggerParameters parms) {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public void Debug(string str) {
            DiagDebug.WriteLine(str);
        }

        public void Error(string str) {
            DiagDebug.WriteLine(str);
        }

        public void Fatal(string str) {
            DiagDebug.WriteLine(str);
        }

        public void Info(string str) {
            DiagDebug.WriteLine(str);
        }

        public void Setup(LoggerParameters parms) {
        }

        public void Teardown() {
        }

        public void Warn(string str) {
            DiagDebug.WriteLine(str);
        }
    }
}