using System;

namespace TaskPlanner.Shared.Data.Components
{
    public class Metadata : Section
    {
        public string? Id { get; set; }
        public string? Owner { get; set; }

        // TODO: Initialize metadata properly.
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }
}
