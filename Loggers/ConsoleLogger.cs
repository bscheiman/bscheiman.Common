#region
using System;

#endregion

namespace bscheiman.Common.Loggers {
    internal class ConsoleLogger : ILogger {
        public void Debug(string str) {
            Console.WriteLine(str);
        }

        public void Error(string str) {
            Console.WriteLine(str);
        }

        public void Fatal(string str) {
            Console.WriteLine(str);
        }

        public void Info(string str) {
            Console.WriteLine(str);
        }

        public void Setup() {
        }

        public void Teardown() {
        }

        public void Warn(string str) {
            Console.WriteLine(str);
        }
    }
}