#region
using System;
using bscheiman.Common.Extensions;

#endregion

namespace bscheiman.Common.Util {
    public static class DateUtil {
        public static readonly DateTime Epoch = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0), DateTimeKind.Utc);

        public static long Now {
            get { return DateTime.UtcNow.ToEpoch(); }
        }

        public static DateTime NowDt {
            get { return DateTime.UtcNow; }
        }

        public static long NowMilli {
            get { return DateTime.UtcNow.ToEpochMilliseconds(); }
        }
    }
}
