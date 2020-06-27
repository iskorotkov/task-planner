using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Components;

namespace TaskPlanner.Client.Shared.Components
{
    public partial class MetricsSection
    {
        [Parameter] public Metric Metric { get; set; }
        [Parameter] public EventCallback<Metric> OnRemove { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
        
        private async Task RemoveSection()
        {
            if (OnRemove.HasDelegate)
            {
                await OnRemove.InvokeAsync(Metric);
            }
        }
    }
}
