using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Sections;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class DeadlinesSection
    {
        [Parameter] public Deadlines? Deadlines { get; set; }
        [Parameter] public EventCallback<Deadlines> OnRemove { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        private async Task RemoveSection()
        {
            if (OnRemove.HasDelegate)
            {
                await OnRemove.InvokeAsync(Deadlines!);
            }
        }
    }
}
