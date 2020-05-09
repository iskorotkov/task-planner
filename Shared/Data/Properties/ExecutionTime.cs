using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Data.Properties
{
    public class ExecutionTime
    {
        public TaskTimeSpan? EstimatedTime { get; set; } = new TaskTimeSpan();
        public TaskTimeSpan? TimeSpent { get; set; } = new TaskTimeSpan();
    }
}
