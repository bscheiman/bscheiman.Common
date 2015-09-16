#region
using System.IO;

#endregion

namespace bscheiman.Common.Extensions {
    public static class StreamExtensions {
        public static T FromJson<T>(this Stream stream) {
            stream.Seek(0, SeekOrigin.Begin);

            using (var sr = new StreamReader(stream)) {
                string raw = sr.ReadToEnd();

                return raw.FromJson<T>();
            }
        }
    }
}
