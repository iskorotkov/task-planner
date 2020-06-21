using System;

namespace TaskPlanner.Shared.Data.Sections
{
    public class Deadlines : OptionalSection
    {
        public DateTime? SoftDeadline { get; set; } = DateTime.Now;
        public DateTime? HardDeadline { get; set; } = DateTime.Now;
    }
}
