#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static class BoolExtensions {
        public static TResult WhenFalse<TResult>(this bool value, Func<TResult> expression) {
            return !value ? expression() : default(TResult);
        }

        public static TResult WhenFalse<TResult>(this bool value, TResult content) {
            return !value ? content : default(TResult);
        }

        public static TResult WhenTrue<TResult>(this bool value, Func<TResult> expression) {
            return value ? expression() : default(TResult);
        }

        public static TResult WhenTrue<TResult>(this bool value, TResult content) {
            return value ? content : default(TResult);
        }
    }
}