#region
using System;
using System.Xml.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static partial class Extensions {
        public static string ValueOrDefault(this XAttribute attr) {
            return ValueOrDefault(attr, string.Empty);
        }

        public static T ValueOrDefault<T>(this XAttribute attr, T defaultValue) {
            if (attr == null)
                return defaultValue;

            var str = attr.Value;
            var type = typeof (T);

            return (T) Convert.ChangeType(str, type);
        }
    }
}