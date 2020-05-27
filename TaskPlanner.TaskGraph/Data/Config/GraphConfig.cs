using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class GraphConfig
    {
        public ReferenceType ReferenceTypes { get; set; } = ReferenceType.All;

        public Position Offsets { get; set; } = new Position(20, 20);
        public Position Intervals { get; set; } = new Position(90, 60);
        public Dimensions Dimensions { get; set; } = new Dimensions(120, 160);

        public Padding ContentPadding { get; set; } = new Padding(8, 16);
        public List<NodeElement> Elements { get; set; } = new List<NodeElement>
        {
            new NodeElement(new Position(0, 0), 20),
            new NodeElement(new Position(0, 20), 20),

            new NodeElement(new Position(0, 20), 20),
            new NodeElement(new Position(0, 20), 20),
            new NodeElement(new Position(0, 20), 20),
            new NodeElement(new Position(0, 20), 20),
            new NodeElement(new Position(0, 20), 20),
            new NodeElement(new Position(0, 20), 20)
        };

        public NodeElement EdgeLabel { get; set; } = new NodeElement(new Position(-10, -5), 16);
    }
}
