using System;
using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TaskTimeSpan ToTaskTimeSpan(this TimeSpan span)
        {
            if (span.TotalSeconds < 60)
            {
                return new TaskTimeSpan((int)span.TotalSeconds, TimePeriod.Seconds);
            }

            if (span.TotalMinutes < 60)
            {
                return new TaskTimeSpan((int)span.TotalMinutes, TimePeriod.Minutes);
            }

            if (span.TotalHours < 24)
            {
                return new TaskTimeSpan((int)span.TotalHours, TimePeriod.Hours);
            }

            if (span.TotalDays < 7)
            {
                return new TaskTimeSpan((int)span.TotalDays, TimePeriod.Days);
            }

            const int daysInMonth = 30;
            if (span.TotalDays < daysInMonth)
            {
                return new TaskTimeSpan((int)span.TotalDays / 7, TimePeriod.Weeks);
            }

            const int daysInYear = 365;
            if (span.TotalDays < daysInYear)
            {
                return new TaskTimeSpan((int)span.TotalDays / daysInMonth, TimePeriod.Months);
            }

            return new TaskTimeSpan((int)span.TotalDays / daysInYear, TimePeriod.Years);
        }
    }
}
