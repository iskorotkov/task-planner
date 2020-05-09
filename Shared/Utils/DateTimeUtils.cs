using System;

namespace TaskPlanner.Shared.Utils
{
    public static class DateTimeUtils
    {
        public static int DaysInCurrentMonth()
        {
            var today = DateTime.Today;
            return DateTime.DaysInMonth(today.Year, today.Month);
        }

        public static int DaysInCurrentYear()
        {
            var today = DateTime.Today;
            return DateTime.IsLeapYear(today.Year) ? 366 : 365;
        }
    }
}
