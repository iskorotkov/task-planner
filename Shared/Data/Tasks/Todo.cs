using System.Collections.Generic;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Sections;

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

        public List<Reference> References { get; set; } = new List<Reference>();
    }
}
