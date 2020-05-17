using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class PlacementNode
    {
        public Todo Task { get; set; }
        public Position Position { get; set; }
    }
}
