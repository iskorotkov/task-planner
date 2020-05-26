using System;
using System.Collections.Generic;
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

        public Todo Task { get; }
        public Position Position { get; }

        public override bool Equals(object? obj)
        {
            return obj is PlacementNode node &&
                   EqualityComparer<Todo>.Default.Equals(Task, node.Task) &&
                   EqualityComparer<Position>.Default.Equals(Position, node.Position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Task, Position);
        }
    }
}
