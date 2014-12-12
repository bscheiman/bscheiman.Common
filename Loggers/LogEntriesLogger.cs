#region
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace bscheiman.Common.Loggers {
    public class LogEntriesLogger : ILogger {
        public BlockingCollection<string> Queue = new BlockingCollection<string>();
        public string Token { get; set; }
        public bool Running { get; set; }

        public LogEntriesLogger(string token) {
            Token = token;
        }

        public void Debug(string str) {
            foreach (var s in str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Queue.Add(string.Format("{0} {1}", Token, str));
        }

        public void Error(string str) {
            foreach (var s in str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Queue.Add(string.Format("{0} {1}", Token, str));
        }

        public void Fatal(string str) {
            foreach (var s in str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Queue.Add(string.Format("{0} {1}", Token, str));
        }

        public void Info(string str) {
            foreach (var s in str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Queue.Add(string.Format("{0} {1}", Token, str));
        }

        public void Setup() {
            Running = true;

            new Thread(SendMessages) {
                Name = "LogEntries Background",
                IsBackground = true
            }.Start();
        }

        public void Teardown() {
            Running = false;

            Queue.CompleteAdding();
        }

        public void Warn(string str) {
            foreach (var s in str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Queue.Add(string.Format("{0} {1}", Token, str));
        }

        internal void SendMessages() {
            try {
                using (var client = new TcpClient("data.logentries.com", 80)) {
                    client.NoDelay = true;

                    using (var stream = client.GetStream())
                    using (var writer = new StreamWriter(stream)) {
                        foreach (var str in Queue.GetConsumingEnumerable()) {
                            writer.WriteLine(str);
                            writer.Flush();
                        }
                    }
                }
            } catch {
                Thread.Sleep(5000);
                SendMessages();
            }
        }
    }
}