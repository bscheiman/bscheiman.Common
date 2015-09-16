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
        public static T Exception<T>(Func<T> act, T def = default(T)) {
            try {
                return act();
            } catch {
                return def;
            }
        }

        /// <summary>
        /// Ignores any exception.
        /// </summary>
        /// <param name="act">Action to execute</param>
        public static void Exception(Action act) {
            try {
                act();
            } catch {
            }
        }

        public static void Exception<T>(Action act) where T : Exception {
            try {
                act();
            } catch (T) {
            }
        }

        public static void Exception<T, TV>(Action act) where T : Exception where TV : Exception {
            try {
                act();
            } catch (T) {
            } catch (TV) {
            }
        }

        public static void Exception<T, TV, TTV>(Action act) where T : Exception where TV : Exception where TTV : Exception {
            try {
                act();
            } catch (T) {
            } catch (TV) {
            } catch (TTV) {
            }
        }
    }
}
