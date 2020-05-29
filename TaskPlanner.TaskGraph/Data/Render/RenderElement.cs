using System;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderElement
    {
        public RenderElement(Position position, Dimensions dimensions)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
        }

        public Position Position { get; }
        public Dimensions Dimensions{ get; }

        protected bool Equals(RenderElement other)
        {
            return Position.Equals(other.Position)
                && Dimensions.Equals(other.Dimensions);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((RenderElement) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Dimensions);
        }
    }
}
