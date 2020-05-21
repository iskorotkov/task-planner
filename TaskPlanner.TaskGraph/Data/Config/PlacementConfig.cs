using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class PlacementConfig
    {
        public ReferenceType ReferenceTypes { get; set; } = ReferenceType.All;
    }
}
