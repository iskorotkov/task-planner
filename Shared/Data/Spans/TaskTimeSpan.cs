using TaskPlanner.Shared.Extensions;

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

        public override string ToString()
        {
            return this.ToLongString();
        }
    }
}
