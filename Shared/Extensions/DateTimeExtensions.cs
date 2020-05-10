using System;
using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsOverdue(this DateTime deadline)
        {
            return deadline < DateTime.Now;
        }

        public static string GetTimeLeftMessage(this DateTime deadline, bool shortVersion = true)
        {
            if (deadline.IsOverdue())
            {
                return "overdue";
            }
            else
            {
                var timeLeft = deadline - DateTime.Now;
                var timeSpan = timeLeft.ToTaskTimeSpan();
                var str = shortVersion ? timeSpan.ToShortString() : timeSpan.ToLongString();
                return str + " left";
            }
        }
    }
}
