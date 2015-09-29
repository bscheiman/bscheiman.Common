#region
using System;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    public class FuncLogger : ILogger {
        public Action<string> LogFunction { get; set; }

        public FuncLogger(Action<string> logFunction) {
            LogFunction = logFunction;
        }

        public bool CanUse(LoggerParameters parms) {
            return true;
        }

        public void Debug(string str) {
            LogFunction(str);
        }

        public void Error(string str) {
            LogFunction(str);
        }

        public void Fatal(string str) {
            LogFunction(str);
        }

        public void Info(string str) {
            LogFunction(str);
        }

        public void Setup(LoggerParameters parms) {
        }

        public void Teardown() {
        }

        public void Warn(string str) {
            LogFunction(str);
        }
    }
}
