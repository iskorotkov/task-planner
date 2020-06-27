using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Components;

namespace TaskPlanner.Client.Shared.Components
{
    public partial class DeadlinesSection
    {
        [Parameter] public Deadline Deadline { get; set; }
        [Parameter] public EventCallback<Deadline> OnRemove { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        private async Task RemoveSection()
        {
            if (OnRemove.HasDelegate)
            {
                await OnRemove.InvokeAsync(Deadline);
            }
        }
    }
}
