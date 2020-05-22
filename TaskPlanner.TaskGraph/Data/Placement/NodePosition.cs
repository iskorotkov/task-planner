using System;
using TaskPlanner.TaskGraph.Data.Abstract;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class NodePosition
    {
        private AbstractNode Node { get; }
        private int Row { get; }
        private int Column { get; }

        public NodePosition(AbstractNode node, int row, int column)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Row = row;
            Column = column;
        }

        public void Deconstruct(out AbstractNode node, out int row, out int column)
        {
            node = Node;
            row = Row;
            column = Column;
        }
    }
}
