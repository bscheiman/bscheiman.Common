#region
using System;
using bscheiman.Common.Extensions;

#endregion

namespace bscheiman.Common.Util {
    public static class DateUtil {
        public static long Now {
            get { return DateTime.UtcNow.ToEpoch(); }
        }

        public static DateTime NowDt {
            get { return DateTime.UtcNow; }
        }
    }
}