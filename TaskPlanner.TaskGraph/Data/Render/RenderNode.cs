using System;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderNode
    {
        public RenderNode(Todo task, Position position, Dimensions dimensions, List<RenderElement> elements)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public Todo Task { get; }
        public Position Position { get; }
        public Dimensions Dimensions { get; }
        public List<RenderElement> Elements { get; }

        public override bool Equals(object? obj)
        {
            return obj is RenderNode node &&
                   EqualityComparer<Todo>.Default.Equals(Task, node.Task) &&
                   EqualityComparer<Position>.Default.Equals(Position, node.Position) &&
                   EqualityComparer<Dimensions>.Default.Equals(Dimensions, node.Dimensions) &&
                   EqualityComparer<List<RenderElement>>.Default.Equals(Elements, node.Elements);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Task, Position, Dimensions, Elements);
        }
    }
}
