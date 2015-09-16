#region
using System;
using System.Xml.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static class XElementExtensions {
        public static string ValueOrDefault(this XElement elem) {
            elem.ThrowIfNull("elem");

            return ValueOrDefault(elem, string.Empty);
        }

        public static string ValueOrDefault(this XElement elem, string defaultValue) {
            elem.ThrowIfNull("elem");

            return elem == null ? defaultValue : elem.Value;
        }

        public static T ValueOrDefault<T>(this XElement elem) {
            elem.ThrowIfNull("elem");

            return elem == null ? default(T) : (T) Convert.ChangeType(elem.Value, typeof (T));
        }
    }
}
