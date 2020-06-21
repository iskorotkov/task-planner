using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Sections;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class MetricsSection
    {
        [Parameter] public Metrics? Metrics { get; set; }
        [Parameter] public EventCallback<Metrics> OnRemove { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
        
        private async Task RemoveSection()
        {
            if (OnRemove.HasDelegate)
            {
                await OnRemove.InvokeAsync(Metrics!);
            }
        }
    }
}
