#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static class IConvertibleExtensions {
        public static T To<T>(this IConvertible obj) {
            return (T) Convert.ChangeType(obj, typeof (T));
        }
    }
}