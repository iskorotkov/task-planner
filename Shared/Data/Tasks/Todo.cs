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

        public List<OptionalSection> Sections { get; set; } = new List<OptionalSection>();
        public List<Reference> References { get; set; } = new List<Reference>();
    }
}
