#region
using System.Net.Http;
using System.Threading.Tasks;
using bscheiman.Common.Extensions;
using bscheiman.Common.Objects;

#endregion

namespace bscheiman.Common.Loggers {
    public class SlackLogger : ILogger {
        public string Token { get; set; }

        public bool CanUse(LoggerParameters parms) {
            return Token.IsNotNullOrEmpty();
        }

        public async void Debug(string str) {
            await Send(str);
        }

        public async void Error(string str) {
            await Send(str);
        }

        public async void Fatal(string str) {
            await Send(str);
        }

        public async void Info(string str) {
            await Send(str);
        }

        public void Setup(LoggerParameters parms) {
        }

        public void Teardown() {
        }

        public async void Warn(string str) {
            await Send(str);
        }

        internal async Task<bool> Send(string text, string channel = "#logs", string username = "papaia.co", string emoji = ":tram:") {
            using (var httpClient = new HttpClient()) {
                var obj = new {
                    channel,
                    username,
                    text,
                    icon_emoji = emoji
                };

                var res =
                    await
                        httpClient.PostAsync(string.Format("https://hooks.slack.com/services/{0}", Token), new StringContent(obj.ToJson()));
                string msg = await res.Content.ReadAsStringAsync();

                return msg == "ok";
            }
        }
    }
}
