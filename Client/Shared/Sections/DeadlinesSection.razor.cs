using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Properties;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class DeadlinesSection
    {
        [Parameter] public Deadlines Deadlines { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
