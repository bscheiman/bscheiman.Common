#region
using System;
using bscheiman.Common.Helpers;

#endregion

namespace bscheiman.Common.Extensions {
    public static class IConvertibleExtensions {
        public static T To<T>(this IConvertible obj, T defValue = default(T)) {
            obj.ThrowIfNull("obj");

            return Ignore.Exception(() => (T) Convert.ChangeType(obj, typeof (T)), defValue);
        }
    }
}