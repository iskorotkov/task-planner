using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderNode
    {
        public Todo Task { get; set; }
        public Position Position { get; set; }
        public Dimensions Dimensions { get; set; }
    }
}
