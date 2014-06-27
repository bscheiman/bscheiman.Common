#region
using System;
using System.Collections;
using System.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        /// <summary>
        /// Generates a query-string-like representation of the object. Useful for REST queries.
        /// </summary>
        /// <returns>The query string.</returns>
        /// <param name="request">Object to reflect.</param>
        /// <param name="separator">Separator for array-based fields.</param>
        public static string ToQueryString(this object request, string separator = ",") {
            if (request == null)
                throw new ArgumentNullException("request");

            var properties =
                request.GetType()
                    .GetProperties()
                    .Where(x => x.CanRead && x.CanWrite)
                    .Where(x => x.GetValue(request, null) != null)
                    .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            var propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable).Select(x => x.Key).ToList();

            foreach (var key in propertyNames) {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType ? valueType.GetGenericArguments()[0] : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof (string)) {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            return string.Join("&",
                properties.Select(x => string.Concat(Uri.EscapeDataString(x.Key), "=", Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}