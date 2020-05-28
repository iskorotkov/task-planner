using System;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Data.Config
{
    public class NodeElement
    {
        public NodeElement(Position offset, int maxLetters, bool nextLine = true)
        {
            Offset = offset ?? throw new System.ArgumentNullException(nameof(offset));
            if (maxLetters < 0)
            {
                throw new ArgumentException("Max width can't be negative.", nameof(maxLetters));
            }
            MaxLetters = maxLetters;
            NextLine = nextLine;
        }

        public Position Offset { get; }
        public int MaxLetters { get; }
        public bool NextLine { get; }
    }
}
