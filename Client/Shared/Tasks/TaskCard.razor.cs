using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskCard
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
#pragma warning restore 8618

#pragma warning disable 8618
        [Parameter] public Todo Todo { get; set; }
#pragma warning restore 8618

        [Parameter(CaptureUnmatchedValues = true)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        protected override void OnParametersSet()
        {
            if (Todo == null)
            {
                throw new ArgumentException(nameof(Todo));
            }
        }

        private void CardClicked(MouseEventArgs obj)
        {
            NavigationManager.NavigateTo($"/tasks/update/{Todo!.Guid}");
        }
    }
}
