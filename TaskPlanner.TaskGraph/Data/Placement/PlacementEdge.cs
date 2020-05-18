using System;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class PlacementEdge
    {
        public PlacementEdge(Position from, Position to, ReferenceType type)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Type = type;
        }

        public Position From { get; set; }
        public Position To { get; set; }
        public ReferenceType Type { get; set; }
    }
}
