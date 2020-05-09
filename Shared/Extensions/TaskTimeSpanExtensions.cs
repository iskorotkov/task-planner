using System;
using TaskPlanner.Shared.Data.Spans;
using TaskPlanner.Shared.Utils;

namespace TaskPlanner.Shared.Extensions
{
    public static class TaskTimeSpanExtensions
    {
        public static TimeSpan ToTimeSpan(this TaskTimeSpan timeSpan)
        {
            return timeSpan.Amount * timeSpan.Period switch
            {
                TimePeriod.Seconds => TimeSpan.FromSeconds(1),
                TimePeriod.Minutes => TimeSpan.FromMinutes(1),
                TimePeriod.Hours => TimeSpan.FromHours(1),
                TimePeriod.Days => TimeSpan.FromDays(1),
                TimePeriod.Weeks => TimeSpan.FromDays(7),
                TimePeriod.Months => TimeSpan.FromDays(DateTimeUtils.DaysInCurrentMonth()),
                TimePeriod.Years => TimeSpan.FromDays(DateTimeUtils.DaysInCurrentYear()),
                _ => throw new ArgumentException($"Provided variant of {nameof(TimePeriod)} isn't supported."),
            };
        }

        public static string ToShortString(this TaskTimeSpan timeSpan)
        {
            return timeSpan.Amount + " " + timeSpan.Period.ToShortString();
        }

        public static string ToLongString(this TaskTimeSpan timeSpan)
        {
            return timeSpan.Amount + " " + timeSpan.Period.ToLongString();
        }
    }
}
