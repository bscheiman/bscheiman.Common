#region
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using bscheiman.Common.Helpers;
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
        public static TV To<T, TV>(this T obj, TV defValue = default(TV)) {
            obj.ThrowIfNull("obj");

            return Ignore.Exception(() => (TV) Convert.ChangeType(obj, typeof (TV)), defValue);
        }

        /// <summary>
        /// Returns a JSON version of the object
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="settings">Custom settings</param>
        /// <returns>JSON string</returns>
        [DebuggerStepThrough]
        public static string ToJson(this object obj, JsonSerializerSettings settings = null) {
            return settings == null ? JsonConvert.SerializeObject(obj) : JsonConvert.SerializeObject(obj, settings);
        }

        [DebuggerStepThrough]
        public static void With<T>(this T obj, Action<T> act) {
            act(obj);
        }
    }
}
