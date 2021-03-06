﻿#region
using System.Collections.Generic;

#endregion

namespace bscheiman.Common.Extensions {
    public static class DictionaryExtensions {
        public static TV Get<T, TV>(this Dictionary<T, TV> dict, T key, TV defValue = default(TV)) {
            dict.ThrowIfNull("dict");
            key.ThrowIfNull("key");

            return dict.ContainsKey(key) ? dict[key] : defValue;
        }
    }
}
