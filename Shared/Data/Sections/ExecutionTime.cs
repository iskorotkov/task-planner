using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Data.Sections
{
    public class ExecutionTime : OptionalSection
    {
        public TaskTimeSpan? EstimatedTime { get; set; } = new TaskTimeSpan();
        public TaskTimeSpan? TimeSpent { get; set; } = new TaskTimeSpan();
    }
}
