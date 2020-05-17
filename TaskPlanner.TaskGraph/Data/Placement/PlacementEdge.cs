using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class PlacementEdge
    {
        public Position From { get; set; }
        public Position To { get; set; }
    }
}
