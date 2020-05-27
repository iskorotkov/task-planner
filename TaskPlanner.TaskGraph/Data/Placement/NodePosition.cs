using System;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Abstract;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class NodePosition
    {
        public NodePosition(AbstractNode node, Position position)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public AbstractNode Node { get; }
        public Position Position { get; }

        public void Deconstruct(out AbstractNode node, out Position position)
        {
            node = Node;
            position = Position;
        }
    }
}
