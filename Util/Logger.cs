#region
using System;
using System.Collections.Generic;
using System.Linq;
using bscheiman.Common.Extensions;
using bscheiman.Common.Loggers;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Util {
    public static class Log {
        internal static readonly List<ILogger> Loggers = new List<ILogger>();

        internal static LoggerParameters DefaultConfig => new LoggerParameters();

        private static bool Initialized { get; set; }

        public static void Debug(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«DEBUG» {0}".FormatWith(str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Debug(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«DEBUG» {0}".FormatWith(str);

            foreach (var l in Loggers)
                l.Debug(str);
        }

        public static void Error(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«ERROR» {0}".FormatWith(str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Error(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«ERROR» {0}".FormatWith(str);

            foreach (var l in Loggers)
                l.Error(str);
        }

        public static void Fatal(string str, Exception e) {
            str.ThrowIfNull("str");
            e.ThrowIfNull("e");

            if (!Initialized)
                Setup();

            str = "«FATAL» {0}".FormatWith(str.FormatWith(e));

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Fatal(Exception e) {
            e.ThrowIfNull("e");

            if (!Initialized)
                Setup();

            string str = "«FATAL» {0}".FormatWith(e);

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Fatal(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«FATAL» {0}".FormatWith(str);

            foreach (var l in Loggers)
                l.Fatal(str);
        }

        public static void Info(string str, params object[] objs) {
            str.ThrowIfNull("str");
            objs.ThrowIfNull("objs");

            if (!Initialized)
                Setup();

            str = "«INFO» {0}".FormatWith(str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Info(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«INFO» {0}".FormatWith(str);

            foreach (var l in Loggers)
                l.Info(str);
        }

        public static void Setup() {
            if (Initialized) {
                Debug("Already initialized...");

                return;
            }

            Setup(DefaultConfig);
        }

        public static void Setup(LoggerParameters parms, params ILogger[] extraLoggers) {
            parms.ThrowIfNull("parms");

            if (Initialized) {
                Debug("Already initialized...");

                return;
            }

            var loggers = new List<ILogger> {
                new DebugLogger(),
                new LogEntriesLogger()
            };

            if (extraLoggers != null)
                loggers.AddRange(extraLoggers);

            foreach (var logger in loggers.Where(logger => logger != null && logger.CanUse(parms)))
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

            str = "«WARN» {0}".FormatWith(str.FormatWith(objs));

            foreach (var l in Loggers)
                l.Warn(str);
        }

        public static void Warn(string str) {
            str.ThrowIfNull("str");

            if (!Initialized)
                Setup();

            str = "«WARN» {0}".FormatWith(str);

            foreach (var l in Loggers)
                l.Warn(str);
        }
    }
}
