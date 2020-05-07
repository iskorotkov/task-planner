using TaskPlanner.Shared.Data.Tasks.Properties;

namespace TaskPlanner.Shared.Data.Tasks
{
    public class Todo
    {
        public Metadata Metadata { get; set; } = new Metadata();
        public Content Content { get; set; } = new Content();
        public Participants Participants { get; set; } = new Participants();
        
        public ExecutionTime? ExecutionTime { get; set; }
        public Deadlines? Deadlines { get; set; }
        public Iterations? Iterations {get;set;}
        public Metrics? Metrics { get; set; }
    }
}
