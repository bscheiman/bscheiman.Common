#region
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        /// <summary>
        /// Generates a FormValues representation of the object. Useful for POST queries.
        /// </summary>
        /// <returns>The form values.</returns>
        /// <param name="request">Object to reflect.</param>
        /// <param name="separator">Separator for array-based fields.</param>
        /// <param name="readAndWrite">Whether to use read/write properties or only read.</param>
        public static NameValueCollection ToFormValues(this object request, string separator = ",", bool readAndWrite = true) {
            if (request == null)
                throw new ArgumentNullException("request");

            var properties =
                request.GetType()
                    .GetProperties()
                    .Where(x => readAndWrite ? x.CanRead && x.CanWrite : x.CanRead)
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

            var values = new NameValueCollection();

            foreach (var p in properties)
                values.Add(p.Key, p.Value.ToString());

            return values;
        }

        /// <summary>
        /// Returns a JSON version of the string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON string</returns>
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Generates a query-string-like representation of the object. Useful for REST queries.
        /// </summary>
        /// <returns>The query string.</returns>
        /// <param name="request">Object to reflect.</param>
        /// <param name="separator">Separator for array-based fields.</param>
        /// <param name="readAndWrite">Whether to use read/write properties or only read.</param>
        public static string ToQueryString(this object request, string separator = ",", bool readAndWrite = true) {
            if (request == null)
                throw new ArgumentNullException("request");

            var properties =
                request.GetType()
                    .GetProperties()
                    .Where(x => readAndWrite ? x.CanRead && x.CanWrite : x.CanRead)
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

            return "?" +
                   string.Join("&",
                       properties.Select(x => string.Concat(Uri.EscapeDataString(x.Key), "=", Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}