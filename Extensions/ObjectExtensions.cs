#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

#endregion

namespace bscheiman.Common.Extensions {
    public static class ObjectExtensions {
        [DebuggerStepThrough]
        public static T As<T>(this object item) where T : class {
            return item as T;
        }

        [DebuggerStepThrough]
        public static bool Between<T>(this T target, T start, T end) where T : IComparable {
            target.ThrowIfNull("target");
            start.ThrowIfNull("start");
            end.ThrowIfNull("end");

            if (start.CompareTo(end) == 1)
                return target.CompareTo(end) >= 0 && target.CompareTo(start) <= 0;

            return target.CompareTo(start) >= 0 && target.CompareTo(end) <= 0;
        }

        [DebuggerStepThrough]
        public static string GetMemberName<T, TResult>(this T anyObject, Expression<Func<T, TResult>> expression) {
            return ((MemberExpression) expression.Body).Member.Name;
        }

        [DebuggerStepThrough]
        public static bool Is<T>(this object item) where T : class {
            return item is T;
        }

        [DebuggerStepThrough]
        public static bool IsIn<T>(this T source, params T[] list) {
            source.ThrowIfNull("source");

            return list.Contains(source);
        }

        [DebuggerStepThrough]
        public static bool IsNot<T>(this object item) where T : class {
            return !(item.Is<T>());
        }

        [DebuggerStepThrough]
        public static bool IsNotIn<T>(this T source, params T[] list) {
            return !source.IsIn(list);
        }

        [DebuggerStepThrough]
        public static bool IsNull(this object o) {
            return o == null;
        }

        [DebuggerStepThrough]
        public static TReturn NullOr<TIn, TReturn>(this TIn obj, Func<TIn, TReturn> func, TReturn elseValue = default(TReturn))
            where TIn : class {
            return obj != null ? func(obj) : elseValue;
        }

        [DebuggerStepThrough]
        public static T Safe<T>(this T obj) where T : new() {
            if (obj == null)
                obj = new T();

            return obj;
        }

        [DebuggerStepThrough]
        public static void ThrowIf<T>(this T obj, bool fail, string error, string parameterName) {
            if (fail)
                throw new ArgumentException(parameterName);
        }

        [DebuggerStepThrough]
        public static void ThrowIfNull<T>(this T obj, string parameterName) {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }
        
        [DebuggerStepThrough]
        public static Dictionary<string, object> ToDictionary(this object o) {
            return
                o.GetType()
                 .GetProperties()
                 .Where(propertyInfo => propertyInfo.GetIndexParameters().Length == 0)
                 .ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(o, null));
        }

        /// <summary>
        /// Generates a FormValues representation of the object. Useful for POST queries.
        /// </summary>
        /// <returns>The form values.</returns>
        /// <param name="request">Object to reflect.</param>
        /// <param name="separator">Separator for array-based fields.</param>
        /// <param name="readAndWrite">Whether to use read/write properties or only read.</param>
        [DebuggerStepThrough]
        public static NameValueCollection ToFormValues(this object request, string separator = ",", bool readAndWrite = true) {
            request.ThrowIfNull("request");

            var properties =
                request.GetType()
                       .GetProperties()
                       .Where(x => readAndWrite ? x.CanRead && x.CanWrite : x.CanRead)
                       .Where(x => x.GetValue(request, null) != null)
                       .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            var propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable).Select(x => x.Key).ToList();

            foreach (string key in propertyNames) {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType ? valueType.GetGenericArguments()[0] : valueType.GetElementType();

                if (!valueElemType.IsPrimitive && valueElemType != typeof (string))
                    continue;

                var enumerable = properties[key] as IEnumerable;
                properties[key] = string.Join(separator, enumerable.Cast<object>());
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
        public static string ToQueryString(this object request, string separator = ",", bool readAndWrite = true) {
            request.ThrowIfNull("request");

            var properties =
                request.GetType()
                       .GetProperties()
                       .Where(x => readAndWrite ? x.CanRead && x.CanWrite : x.CanRead)
                       .Where(x => x.GetValue(request, null) != null)
                       .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            var propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable).Select(x => x.Key).ToList();

            foreach (string key in propertyNames) {
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

        [DebuggerStepThrough]
        public static void With<T>(this T obj, Action<T> act) {
            act(obj);
        }
    }
}