#region
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using bscheiman.Common.Extensions;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    internal class LogEntriesLogger : ILogger {
        internal const int Port = 80;
        internal const string Server = "data.logentries.com";
        public static BlockingCollection<string> Queue = new BlockingCollection<string>();
        public bool Running { get; set; }
        public string Token { get; set; }

        public bool CanUse(LoggerParameters parms) {
            return parms.LogEntriesToken.IsNotNullOrEmpty();
        }

        public void Debug(string str) {
            if (!Running)
                return;

            foreach (string s in str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray()))
                Queue.Add("{0} {1}".FormatWith(Token, s));
        }

        public void Error(string str) {
            if (!Running)
                return;

            foreach (string s in str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray()))
                Queue.Add("{0} {1}".FormatWith(Token, s));
        }

        public void Fatal(string str) {
            if (!Running)
                return;

            foreach (string s in str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray()))
                Queue.Add("{0} {1}".FormatWith(Token, s));
        }

        public void Info(string str) {
            if (!Running)
                return;

            foreach (string s in str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray()))
                Queue.Add("{0} {1}".FormatWith(Token, s));
        }

        public void Setup(LoggerParameters parms) {
            Token = parms.LogEntriesToken;
            Running = true;

            new Thread(SendMessages) {
                Name = "LogEntries Background",
                IsBackground = false
            }.Start();
        }

        public void Teardown() {
            Running = false;

            Queue.CompleteAdding();
        }

        public void Warn(string str) {
            if (!Running)
                return;

            foreach (string s in str.SplitRemoveEmptyEntries(Environment.NewLine.ToCharArray()))
                Queue.Add("{0} {1}".FormatWith(Token, s));
        }

        internal void SendMessages() {
            //try {
            using (var client = new TcpClient(Server, Port)) {
                client.NoDelay = true;

                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream)) {
                    foreach (string str in Queue.GetConsumingEnumerable()) {
                        writer.WriteLine(str);
                        writer.Flush();
                    }
                }
            }
            //} catch {
            //    Thread.Sleep(2500);
            //    SendMessages();
            //}
        }
    }
}