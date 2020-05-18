using System;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderNode
    {
        public RenderNode(Todo task, Position position, Dimensions dimensions)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
        }

        public Todo Task { get; set; }
        public Position Position { get; set; }
        public Dimensions Dimensions { get; set; }
    }
}
