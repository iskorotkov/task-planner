using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace TaskPlanner.Client.Shared.Templates
{
    public partial class SectionCardTemplate<TValue>
    {
        [Parameter] public TValue Value { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
