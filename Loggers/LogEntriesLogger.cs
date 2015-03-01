#region
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using bscheiman.Common.Extensions;
using bscheiman.Common.Objects;
using Sockets.Plugin;

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

            Task.Factory.StartNew(SendMessages);
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

        internal async void SendMessages() {
            try {
                using (var client = new TcpSocketClient()) {
                    await client.ConnectAsync(Server, Port);

                    using (var writer = new StreamWriter(client.WriteStream)) {
                        foreach (string str in Queue.GetConsumingEnumerable()) {
                            writer.WriteLine(str);
                            writer.Flush();
                        }
                    }
                }
            } catch {
                SendMessages();
            }
        }
    }
}