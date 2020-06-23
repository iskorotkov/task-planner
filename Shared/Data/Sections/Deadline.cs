using System;

namespace TaskPlanner.Shared.Data.Sections
{
    public class Deadline : OptionalSection
    {
        public string Title { get; set; } = "Deadline";
        public DateTime Time { get; set; } = DateTime.Now.AddDays(1);
    }
}
