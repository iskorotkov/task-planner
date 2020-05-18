using System;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class PlacementNode
    {
        public PlacementNode(Todo task, Position position)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Todo Task { get; set; }
        public Position Position { get; set; }
    }
}
