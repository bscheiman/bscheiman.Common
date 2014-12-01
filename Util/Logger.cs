#region
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using bscheiman.Common.Objects;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net.Util;

#endregion

namespace bscheiman.Common.Util {
    public static class Log {
        internal static string Caller = string.Format("[{0}] ", Process.GetCurrentProcess().ProcessName);
        internal static ILog Logger = LogManager.GetLogger(typeof (Log));

        internal static LoggerParameters DefaultConfig {
            get {
                return new LoggerParameters {
                    Pattern = "[%level%] %m%n",
                    LogEntriesToken = "",
                    Debug = true
                };
            }
        }

        private static bool Initialized { get; set; }

        #region Logging functions
        public static void Debug(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("{0}{1}", Caller, str);

            Logger.DebugFormat(str, objs);
        }

        public static void Error(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("{0}{1}", Caller, str);

            Logger.ErrorFormat(str, objs);
        }

        public static void Fatal(string str, Exception e) {
            if (!Initialized)
                Setup();

            str = string.Format("{0}{1}", Caller, str);

            Logger.Fatal(str, e);
        }

        public static void Info(string str, params object[] objs) {
            if (!Initialized)
                Setup();

            str = string.Format("{0}{1}", Caller, str);

            Logger.InfoFormat(str, objs);
        }

        public static void Warn(string str, params object[] objs) {
            str = Caller + str;

            Logger.WarnFormat(str, objs);
        }
        #endregion

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        public static void Setup() {
            Setup(DefaultConfig);
        }

        public static void Setup(LoggerParameters parms) {
            if (Initialized) {
                Debug("Already initialized...");

                return;
            }

            var hierarchy = (Hierarchy) LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();

            var patternLayout = new PatternLayout {
                ConversionPattern = parms.Pattern
            };
            patternLayout.ActivateOptions();

            var appenders = new List<AppenderSkeleton>();

            if (GetConsoleWindow() != IntPtr.Zero) {
                appenders.Add(new TraceAppender {
                    Layout = patternLayout
                });
            } else {
                appenders.Add(new ConsoleAppender {
                    Layout = patternLayout
                });
            }

            if (!string.IsNullOrEmpty(parms.LogEntriesToken)) {
                appenders.Add(new LogentriesAppender {
                    Layout = patternLayout,
                    UseSsl = false,
                    ImmediateFlush = true,
                    Debug = parms.Debug,
                    Token = parms.LogEntriesToken,
                    UseHttpPut = false
                });
            }

            hierarchy.Root.Level = Level.All;

            LogLog.InternalDebugging = parms.Debug;

            foreach (var app in appenders) {
                BasicConfigurator.Configure(app);
                app.ActivateOptions();

                hierarchy.Root.AddAppender(app);
            }

            hierarchy.Configured = true;
            Initialized = true;
        }
    }
}