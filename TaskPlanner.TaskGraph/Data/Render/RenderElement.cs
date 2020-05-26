using System;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderElement
    {
        public RenderElement(Position position, int maxLetters)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            MaxLetters = maxLetters;
        }

        public Position Position { get; }
        public int MaxLetters { get; }

        protected bool Equals(RenderElement other)
        {
            return Position.Equals(other.Position) && MaxLetters.Equals(other.MaxLetters);
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
            return HashCode.Combine(Position, MaxLetters);
        }
    }
}
