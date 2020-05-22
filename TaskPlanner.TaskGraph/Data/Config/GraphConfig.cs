using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class GraphConfig
    {
        public ReferenceType ReferenceTypes { get; set; } = ReferenceType.All;

        public int LeftOffset { get; set; } = 20;
        public int TopOffset { get; set; } = 20;
        public int HorizontalInterval { get; set; } = 30;
        public int VerticalInterval { get; set; } = 30;

        public int NodeWidth { get; set; } = 120;
        public int NodeHeight { get; set; } = 160;
    }
}
