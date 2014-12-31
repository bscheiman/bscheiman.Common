#region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using bscheiman.Common.Extensions;
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
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«DEBUG/{0}» {1}".FormatWith(Caller, str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Debug(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«DEBUG/{0}» {1}".FormatWith(Caller, str);

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Error(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«ERROR/{0}» {1}".FormatWith(Caller, str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Error(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«ERROR/{0}» {1}".FormatWith(Caller, str);

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Fatal(string str, Exception e) {
            str.ThrowIfNull("str");
            e.ThrowIfNull("e");

            if (!Initialized)
                Setup();

            str = "«FATAL/{0}» {1}".FormatWith(Caller, str.FormatWith(e));

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Fatal(Exception e) {
            e.ThrowIfNull("e");

            if (!Initialized)
                Setup();

            string str = "«FATAL/{0}» {1}".FormatWith(Caller, e);

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Fatal(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«FATAL/{0}» {1}".FormatWith(Caller, str);

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Info(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«INFO/{0}» {1}".FormatWith(Caller, str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Info(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«INFO/{0}» {1}".FormatWith(Caller, str);

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Setup() {
            Setup(DefaultConfig);
        }

        public static void Setup(LoggerParameters parms) {
            parms.ThrowIfNull("parms");

            if (Initialized) {
                Debug("Already initialized...");

                return;
            }

            foreach (
                var logger in
                    typeof (ILogger).GetImplementations()
                                    .Select(type => Activator.CreateInstance(type) as ILogger)
                                    .Where(logger => logger != null && logger.CanUse(parms)))
                Loggers.Add(logger);

            foreach (var l in Loggers)
                l.Setup(parms);

            Initialized = true;
        }

        public static void Teardown() {
            foreach (var l in Loggers)
                l.Teardown();
        }

        public static void Warn(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«WARN/{0}» {1}".FormatWith(Caller, str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Warn(str);
        }

        public static void Warn(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«WARN/{0}» {1}".FormatWith(Caller, str);

            foreach (var l in Loggers)
                l.Warn(str);
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
    }
}