using System;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class NodeElement
    {
        public NodeElement(Position offset, Dimensions dimensions, bool nextLine = true, bool stretchHorizontal = false)
        {
            Offset = offset ?? throw new ArgumentNullException(nameof(offset));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            StretchHorizontal = stretchHorizontal;
            NextLine = nextLine;
        }

        public Position Offset { get; }
        public Dimensions Dimensions { get; }
        public bool NextLine { get; }
        public bool StretchHorizontal { get; }
    }
}
