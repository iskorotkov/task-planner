using System;

namespace TaskPlanner.Shared.Data.Tasks
{
    public class Todo
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
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

        public Todo()
        {
        }

        // TODO: Combine fields into objects or structs.
        public Todo(Guid guid, string owner, string author,
            int complexity, string description, int importance,
            string title, double estimatedTime, double timeSpent,
            DateTime hardDeadline, DateTime softDeadline)
        {
            Guid = guid;
            Owner = owner;
            Author = author;
            Complexity = complexity;
            Description = description;
            Importance = importance;
            Title = title;
            EstimatedTime = estimatedTime;
            TimeSpent = timeSpent;
            HardDeadline = hardDeadline;
            SoftDeadline = softDeadline;
        }
    }
}
