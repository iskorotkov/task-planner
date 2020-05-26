using System;
using System.Collections.Generic;
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

        public Position From { get; }
        public Position To { get; }
        public ReferenceType Type { get; }

        public override bool Equals(object? obj)
        {
            return obj is PlacementEdge edge &&
                   EqualityComparer<Position>.Default.Equals(From, edge.From) &&
                   EqualityComparer<Position>.Default.Equals(To, edge.To) &&
                   Type == edge.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To, Type);
        }
    }
}
