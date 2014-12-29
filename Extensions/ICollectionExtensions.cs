#region
using System.Collections.Generic;

#endregion

namespace bscheiman.Common.Extensions {
    public static class ICollectionExtensions {
        public static void AddRange<T, TS>(this ICollection<T> list, params TS[] values) where TS : T {
            foreach (TS value in values)
                list.Add(value);
        }
    }
}