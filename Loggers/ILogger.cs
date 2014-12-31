#region
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    public interface ILogger {
        bool CanUse(LoggerParameters parms);
        void Debug(string str);
        void Error(string str);
        void Fatal(string str);
        void Info(string str);
        void Setup(LoggerParameters parms);
        void Teardown();
        void Warn(string str);
    }
}