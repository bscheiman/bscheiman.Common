#region
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static class TypeExtensions {
        public static IEnumerable<Type> GetImplementations(this Type type) {
            type.ThrowIfNull("type");
            type.ThrowIf(!type.IsInterface, "Type is not an interface", "type");

            return
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
        }
    }
}