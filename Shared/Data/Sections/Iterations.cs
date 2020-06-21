using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Shared.Data.Sections
{
    public class Iterations : OptionalSection
    {
        public int Executed { get; set; }
        public int Required { get; set; }

        public TaskTimeSpan? TimePerIteration { get; set; } = new TaskTimeSpan();
    }
}