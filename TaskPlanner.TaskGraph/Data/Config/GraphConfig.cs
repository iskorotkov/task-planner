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
            new NodeElement(new Position(0, 0), new Dimensions(104, 8)), // Title
            new NodeElement(new Position(0, 20), new Dimensions(104, 8)), // Description

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 1
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true),

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 2
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true),

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 3
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true),

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 4
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true),

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 5
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true),

            new NodeElement(new Position(0, 20), new Dimensions(8)), // Component 6
            new NodeElement(new Position(10, 0), new Dimensions(8),
                nextLine: false, stretchHorizontal: true)
        };

        public NodeElement EdgeLabel { get; set; } = new NodeElement(new Position(-30, -15), new Dimensions(70, 8));
        public NodeElement BackwardEdgeLabel { get; set; } = new NodeElement(new Position(-30, 5), new Dimensions(70, 8));
    }
}
