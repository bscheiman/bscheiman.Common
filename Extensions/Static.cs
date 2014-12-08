#region
using System.Net;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        static Extensions() {
            var type = typeof (HttpWebRequest);

            foreach (string header in RestrictedHeaders) {
                string propertyName = header.Replace("-", "");
                var headerProperty = type.GetProperty(propertyName);

                HeaderProperties[header] = headerProperty;
            }
        }
    }
}