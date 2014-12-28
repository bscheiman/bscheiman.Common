#region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using bscheiman.Common.Loggers;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Util {
    public static class Log {
        internal static string Caller = Process.GetCurrentProcess().ProcessName;
        internal static readonly List<ILogger> Loggers = new List<ILogger>();

        internal static LoggerParameters DefaultConfig {
            get {
                return new LoggerParameters {
                    LogEntriesToken = ""
                };
            }
        }

        private static bool Initialized { get; set; }

        public static void Debug(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("«DEBUG/{0}» {1}", Caller, string.Format(str, objs));

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Debug(string str) {
            if (!Initialized)
                Setup();

            str = string.Format("«DEBUG/{0}» {1}", Caller, str);

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Error(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("«ERROR/{0}» {1}", Caller, string.Format(str, objs));

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Error(string str) {
            if (!Initialized)
                Setup();

            str = string.Format("«ERROR/{0}» {1}", Caller, str);

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Fatal(string str, Exception e) {
            if (!Initialized)
                Setup();

            str = string.Format("«FATAL/{0}» {1}: {2}", Caller, string.Format(str, e));

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Fatal(string str) {
            if (!Initialized)
                Setup();

            str = string.Format("«FATAL/{0}» {1}: {2}", Caller, str);

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Info(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("«INFO/{0}» {1}", Caller, string.Format(str, objs));

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Info(string str) {
            if (!Initialized)
                Setup();

            str = string.Format("«INFO/{0}» {1}", Caller, str);

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Setup() {
            Setup(DefaultConfig);
        }

        public static void Setup(LoggerParameters parms) {
            if (Initialized) {
                Debug("Already initialized...");

                return;
            }

            if (GetConsoleWindow() != IntPtr.Zero || Process.GetProcessesByName("linqpad").Length > 0)
                Loggers.Add(new ConsoleLogger());

            Loggers.Add(new TraceLogger());

            if (!string.IsNullOrEmpty(parms.LogEntriesToken))
                Loggers.Add(new LogEntriesLogger(parms.LogEntriesToken));

            foreach (var l in Loggers)
                l.Setup();

            Initialized = true;
        }

        public static void Teardown() {
            foreach (var l in Loggers)
                l.Teardown();
        }

        public static void Warn(string str, params object[] objs) {
            str = string.Format("«WARN/{0}» {1}", Caller, string.Format(str, objs));

            foreach (var l in Loggers)
                l.Warn(str);
        }

        public static void Warn(string str) {
            str = string.Format("«WARN/{0}» {1}", Caller, str);

            foreach (var l in Loggers)
                l.Warn(str);
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
    }
}