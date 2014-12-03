#region
using System;
using bscheiman.Common.Extensions;

#endregion

namespace bscheiman.Common.Util {
    public static class DateUtil {
        public static long Now = DateTime.UtcNow.ToEpoch();
    }
}