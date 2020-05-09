using System;

namespace TaskPlanner.Shared.Data.Spans
{
    public class TaskTimeSpan
    {
        public double Amount { get; set; } = 1.0;
        public TimePeriod Period { get; set; } = TimePeriod.Hours;

        public TaskTimeSpan()
        {
        }

        public TaskTimeSpan(double amount, TimePeriod length)
        {
            Amount = amount;
            Period = length;
        }

        public TimeSpan ToTimeSpan()
        {
            return Amount * Period switch
            {
                TimePeriod.Seconds => TimeSpan.FromSeconds(1),
                TimePeriod.Minutes => TimeSpan.FromMinutes(1),
                TimePeriod.Hours => TimeSpan.FromHours(1),
                TimePeriod.Days => TimeSpan.FromDays(1),
                TimePeriod.Weeks => TimeSpan.FromDays(7),
                TimePeriod.Months => TimeSpan.FromDays(DaysInCurrentMonth()),
                TimePeriod.Years => TimeSpan.FromDays(DaysInCurrentYear()),
                _ => throw new ArgumentException($"Provided variant of {nameof(TimePeriod)} isn't supported."),
            };
        }

        public override string ToString()
        {
            return $"{Amount} {Period}";
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
