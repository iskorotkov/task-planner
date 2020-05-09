using System;

namespace TaskPlanner.Shared.Data.Spans
{
    public class TaskTimeSpan
    {
        public double Amount { get; set; } = 1.0;
        public SpanLength Length { get; set; } = SpanLength.Hours;

        public TaskTimeSpan()
        {
        }

        public TaskTimeSpan(double amount, SpanLength length)
        {
            Amount = amount;
            Length = length;
        }

        public TimeSpan ToTimeSpan()
        {
            return Amount * Length switch
            {
                SpanLength.Seconds => TimeSpan.FromSeconds(1),
                SpanLength.Minutes => TimeSpan.FromMinutes(1),
                SpanLength.Hours => TimeSpan.FromHours(1),
                SpanLength.Days => TimeSpan.FromDays(1),
                SpanLength.Weeks => TimeSpan.FromDays(7),
                SpanLength.Months => TimeSpan.FromDays(DaysInCurrentMonth()),
                SpanLength.Years => TimeSpan.FromDays(DaysInCurrentYear()),
                _ => throw new ArgumentException($"Provided variant of {nameof(SpanLength)} isn't supported."),
            };
        }

        private static int DaysInCurrentMonth()
        {
            var today = DateTime.Today;
            return DateTime.DaysInMonth(today.Year, today.Month);
        }

        private static int DaysInCurrentYear()
        {
            var today = DateTime.Today;
            return DateTime.IsLeapYear(today.Year) ? 366 : 365;
        }
    }
}
