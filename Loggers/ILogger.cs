#region

#endregion

namespace bscheiman.Common.Loggers {
    public interface ILogger {
        void Debug(string str);
        void Error(string str);
        void Fatal(string str);
        void Info(string str);
        void Setup();
        void Teardown();
        void Warn(string str);
    }
}