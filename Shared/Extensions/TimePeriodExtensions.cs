using System;
using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Extensions
{
    public static class TimePeriodExtensions
    {
        public static string ToLongString(this TimePeriod period)
        {
            return period switch
            {
                TimePeriod.Seconds => "second(s)",
                TimePeriod.Minutes => "minute(s)",
                TimePeriod.Hours => "hour(s)",
                TimePeriod.Days => "day(s)",
                TimePeriod.Weeks => "week(s)",
                TimePeriod.Months => "month(s)",
                TimePeriod.Years => "year(s)",
                _ => throw new ArgumentException($"Provided {nameof(TimePeriod)} value isn't supported."),
            };
        }

        public static string ToShortString(this TimePeriod period)
        {
            return period switch
            {
                TimePeriod.Seconds => "s",
                TimePeriod.Minutes => "min",
                TimePeriod.Hours => "h",
                TimePeriod.Days => "d",
                TimePeriod.Weeks => "w",
                TimePeriod.Months => "M",
                TimePeriod.Years => "y",
                _ => throw new ArgumentException($"Provided {nameof(TimePeriod)} value isn't supported."),
            };
        }
    }
}
