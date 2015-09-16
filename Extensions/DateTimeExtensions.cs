#region
using System;
using bscheiman.Common.Util;

#endregion

namespace bscheiman.Common.Extensions {
    /// <summary>
    /// Originally from DateTimeExtensions @ http://datetimeextensions.codeplex.com/
    /// </summary>
    public static class DateTimeExtensions {
        /// <summary>
        /// Gets a DateTime representing the first day in the current month.
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current) {
            return current.AddDays(1 - current.Day);
        }

        /// <summary>
        /// Gets a DateTime representing the first specified day in the current month.
        /// </summary>
        /// <param name="current">The current day</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current, DayOfWeek dayOfWeek) {
            var first = current.First();

            if (first.DayOfWeek != dayOfWeek)
                first = first.Next(dayOfWeek);

            return first;
        }

        public static bool IsFuture(this DateTime date, DateTime from) {
            return date.Date > from.Date;
        }

        public static bool IsFuture(this DateTime date) {
            return date.IsFuture(DateTime.Now);
        }

        public static bool IsPast(this DateTime date, DateTime from) {
            return date.Date < from.Date;
        }

        public static bool IsPast(this DateTime date) {
            return date.IsPast(DateTime.Now);
        }

        /// <summary>
        /// Gets a DateTime representing the last day in the current month.
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current) {
            return current.First().AddDays(DateTime.DaysInMonth(current.Year, current.Month) - 1);
        }

        /// <summary>
        /// Gets a DateTime representing the last specified day in the current month.
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current, DayOfWeek dayOfWeek) {
            var last = current.Last();

            while (last.DayOfWeek != dayOfWeek)
                last = last.AddDays(-1);

            return last;
        }

        /// <summary>
        /// Gets a DateTime representing midnight on the current date.
        /// </summary>
        /// <param name="current">The current date</param>
        public static DateTime Midnight(this DateTime current) {
            return new DateTime(current.Year, current.Month, current.Day);
        }

        /// <summary>
        /// Gets a DateTime representing the first date following the current date which falls on the given day of the week.
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The day of week for the next date to get</param>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek) {
            int offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
                offsetDays += 7;

            return current.AddDays(offsetDays);
        }

        /// <summary>
        /// Gets a DateTime representing noon on the current date.
        /// </summary>
        /// <param name="current">The current date</param>
        public static DateTime Noon(this DateTime current) {
            return new DateTime(current.Year, current.Month, current.Day, 12, 0, 0);
        }

        public static DateTime SetTime(this DateTime current, int hour, int minute, int second = 0, int millisecond = 0) {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Returns the specified DateTime as epoch (seconds since Jan 1, 1970)
        /// </summary>
        /// <returns>Epoch</returns>
        /// <param name="dt">DateTime to use</param>
        public static long ToEpoch(this DateTime dt) {
            return (long) (dt - DateUtil.Epoch).TotalSeconds;
        }

        /// <summary>
        /// Returns the specified DateTime as epoch (milliseconds since Jan 1, 1970)
        /// </summary>
        /// <returns>Epoch</returns>
        /// <param name="dt">DateTime to use</param>
        public static long ToEpochMilliseconds(this DateTime dt) {
            return (long) (dt - DateUtil.Epoch).TotalMilliseconds;
        }
    }
}
