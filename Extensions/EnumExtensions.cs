#region
using System;

#endregion

namespace bscheiman.Common.Extensions {
    public static class EnumExtensions {
        /// <summary>
        /// Gets the first matching attribute of type T
        /// </summary>
        /// <returns>The attribute instance</returns>
        /// <param name="enumVal">Enum</param>
        /// <typeparam name="T">Enum to use</typeparam>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof (T), false);

            return (attributes.Length > 0) ? (T) attributes[0] : null;
        }
    }
}