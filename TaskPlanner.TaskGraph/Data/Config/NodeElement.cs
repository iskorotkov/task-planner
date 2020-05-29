using System;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class NodeElement
    {
        public NodeElement(Position offset, Dimensions dimensions, bool nextLine = true)
        {
            Offset = offset ?? throw new System.ArgumentNullException(nameof(offset));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            NextLine = nextLine;
        }

        public Position Offset { get; }
        public Dimensions Dimensions { get; }
        public bool NextLine { get; }
    }
}
