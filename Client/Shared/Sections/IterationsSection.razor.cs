using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Properties;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class IterationsSection
    {
        [Parameter] public Iterations Iterations { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
