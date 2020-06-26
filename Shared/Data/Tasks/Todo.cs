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

        public List<Deadline> Deadlines { get; set; } = new List<Deadline>();
        public List<ExecutionTime> ExecutionTimes { get; set; } = new List<ExecutionTime>();
        public List<Iterations> Iterations { get; set; } = new List<Iterations>();
        public List<Metric> Metrics { get; set; } = new List<Metric>();

        public List<Reference> References { get; set; } = new List<Reference>();
    }
}
