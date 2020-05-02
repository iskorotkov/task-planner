using System;

namespace TaskPlanner.Shared.Data.Tasks
{
    public class Todo
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }

        public int? Importance { get; set; } = null;
        public int? Complexity { get; set; } = null;

        public double? EstimatedTime { get; set; } = null;
        public double? TimeSpent { get; set; } = null;

        public DateTime? SoftDeadline { get; set; } = null;
        public DateTime? HardDeadline { get; set; } = null;
    }
}
