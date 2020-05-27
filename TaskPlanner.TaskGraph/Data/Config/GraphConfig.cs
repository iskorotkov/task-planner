using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class GraphConfig
    {
        public ReferenceType ReferenceTypes { get; set; } = ReferenceType.All;

        public Position Offsets { get; set; } = new Position(20, 20);
        public Position Intervals { get; set; } = new Position(30, 30);
        public Dimensions Dimensions { get; set; } = new Dimensions(120, 160);

        public Padding ContentPadding { get; set; } = new Padding(8);
        public List<NodeElement> ElementsPositions { get; set; } = new List<NodeElement>
        {
            new NodeElement(new Position(20, 20), 10),
            new NodeElement(new Position(20, 40), 10)
        };
    }
}
