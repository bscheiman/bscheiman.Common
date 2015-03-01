#region
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

#endregion

namespace bscheiman.Common.Extensions {
    public static class HttpWebRequestExtensions {
        private static readonly Dictionary<string, PropertyInfo> HeaderProperties =
            new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

        private static readonly string[] RestrictedHeaders = {
            "Accept", "Connection", "Content-Length", "Content-Type", "Date", "Expect", "Host", "If-Modified-Since", "Range", "Referer",
            "Transfer-Encoding", "User-Agent", "Proxy-Connection"
        };

        static HttpWebRequestExtensions() {
            var type = typeof (HttpWebRequest).GetTypeInfo();

            foreach (string header in RestrictedHeaders) {
                string propertyName = header.Replace("-", "");
                var headerProperty = type.GetDeclaredProperty(propertyName);

                HeaderProperties[header] = headerProperty;
            }
        }

        public static void SetRawHeader(this HttpWebRequest request, string name, string value) {
            if (HeaderProperties.ContainsKey(name))
                HeaderProperties[name].SetValue(request, value, null);
            else
                request.Headers[name] = value;
        }
    }
}