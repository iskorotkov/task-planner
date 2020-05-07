using System;

namespace TaskPlanner.Shared.Data.Tasks
{
    public class Todo
    {
        public string? Id { get; set; }
        public string? Owner { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }

        public int? Importance { get; set; }
        public int? Complexity { get; set; }

        public double? EstimatedTime { get; set; }
        public double? TimeSpent { get; set; }

        public DateTime? SoftDeadline { get; set; }
        public DateTime? HardDeadline { get; set; }
    }
}
