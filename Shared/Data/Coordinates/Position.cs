using System;

namespace TaskPlanner.Shared.Data.Coordinates
{
    public class Position
    {
        public Position()
        {

        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Position operator +(Position left, Position right)
        {
            return new Position(left.X + right.X, left.Y + right.Y);
        }

        public static Position operator -(Position left, Position right)
        {
            return new Position(left.X - right.X, left.Y - right.Y);
        }

        public static Position operator *(Position pos, int value)
        {
            return new Position(pos.X * value, pos.Y * value);
        }

        public static Position operator *(int value, Position pos)
        {
            return pos * value;
        }

        public static Position operator /(Position pos, int value)
        {
            return new Position(pos.X / value, pos.Y / value);
        }

        public static Position operator /(int value, Position pos)
        {
            return pos / value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}
