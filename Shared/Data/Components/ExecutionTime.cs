using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Data.Components
{
    public class ExecutionTime : OptionalSection
    {
        public string Title { get; set; } = "Estimated time";
        public TaskTimeSpan Time { get; set; } = new TaskTimeSpan();
    }
}
