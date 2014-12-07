#region
using System;
using System.Xml.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        public static string ValueOrDefault(this XElement attr) {
            return ValueOrDefault(attr, string.Empty);
        }

        public static string ValueOrDefault(this XElement attr, string defaultValue) {
            return attr == null ? defaultValue : attr.Value;
        }

        public static T ValueOrDefault<T>(this XElement elem) {
            return elem == null ? default(T) : (T) Convert.ChangeType(elem.Value, typeof (T));
        }
    }
}