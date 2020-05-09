using System;

namespace TaskPlanner.Shared.Data.Properties
{
    public class Deadlines
    {
        public DateTime? SoftDeadline { get; set; } = DateTime.Now;
        public DateTime? HardDeadline { get; set; } = DateTime.Now;
    }
}
