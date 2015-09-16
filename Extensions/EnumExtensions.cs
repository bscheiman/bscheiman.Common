#region
using System;
using System.Linq;
using System.Reflection;
using bscheiman.Common.Attributes;
using bscheiman.Common.Helpers;

#endregion

namespace bscheiman.Common.Extensions {
    public static class EnumExtensions {
        /// <summary>
        /// Gets the first matching attribute of type T
        /// </summary>
        /// <returns>The attribute instance</returns>
        /// <param name="enumVal">Enum</param>
        /// <typeparam name="TAttribute">Enum to use</typeparam>
        public static TAttribute GetAttributeOfType<TAttribute>(this Enum enumVal) where TAttribute : Attribute {
            var type = enumVal.GetType();
            var info = type.GetTypeInfo();
            string name = Enum.GetName(type, enumVal);

            return info.GetDeclaredField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static string GetDescription(this Enum enumVal) {
            return Ignore.Exception(() => enumVal.GetAttributeOfType<DescriptionAttribute>().Description, "");
        }
    }
}
