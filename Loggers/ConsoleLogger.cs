#region
using System;
using bscheiman.Common.Helpers;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    internal class ConsoleLogger : ILogger {
        public bool CanUse(LoggerParameters parms) {
            return Ignore.Exception(() => {
                                        Console.WriteLine("Checking console...");
                                        return true;
                                    });
        }

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

        public void Setup(LoggerParameters parms) {
        }

        public void Teardown() {
        }

        public void Warn(string str) {
            Console.WriteLine(str);
        }
    }
}