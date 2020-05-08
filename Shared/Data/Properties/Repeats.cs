using System;

namespace TaskPlanner.Shared.Data.Properties
{
    public class Iterations
    {
        public int Executed { get; set; }
        public int Required { get; set; }

        public TimeSpan? TimePerIteration { get; set; }
    }
}
