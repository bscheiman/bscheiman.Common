#region
using System;

#endregion

namespace bscheiman.Common.Helpers {
    public static class Ignore {
        /// <summary>
        /// Runs action() and returns a default value if there's an exception.
        /// </summary>
        /// <param name="act">Action</param>
        /// <param name="def">Default value</param>
        /// <typeparam name="T">Type parameter</typeparam>
        public static T Exception<T>(Func<T> act, T def) {
            try {
                return act();
            } catch {
                return def;
            }
        }

        /// <summary>
        /// Ignores any exception. All those try/catch
        /// </summary>
        /// <param name="act">Action to execute</param>
        public static void Exception(Action act) {
            try {
                act();
            } catch {
            }
        }
    }
}