#region
using System.Collections.Generic;

#endregion

namespace bscheiman.Common.Extensions {
    public static class ICollectionExtensions {
        public static void AddRange<T, TS>(this ICollection<T> list, params TS[] values) where TS : T {
            list.ThrowIfNull("list");
            values.ThrowIfNull("values");

            foreach (var value in values)
                list.Add(value);
        }
    }
}
