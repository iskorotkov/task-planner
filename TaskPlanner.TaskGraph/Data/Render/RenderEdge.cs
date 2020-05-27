using System;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderEdge
    {
        public RenderEdge(Position from, Position to, ReferenceType type, RenderElement label)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Type = type;
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public Position From { get; }
        public Position To { get; }
        public ReferenceType Type { get; }
        public RenderElement Label { get; }

        public override bool Equals(object? obj)
        {
            return obj is RenderEdge edge &&
                   EqualityComparer<Position>.Default.Equals(From, edge.From) &&
                   EqualityComparer<Position>.Default.Equals(To, edge.To) &&
                   Type == edge.Type &&
                   EqualityComparer<RenderElement>.Default.Equals(Label, edge.Label);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To, Type, Label);
        }
    }
}
