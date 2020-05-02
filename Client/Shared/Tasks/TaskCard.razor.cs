using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskCard
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter] public Todo? Todo { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnParametersSet()
        {
            if (Todo == null)
            {
                throw new ArgumentException(nameof(Todo));
            }
        }

        private void CardClicked(MouseEventArgs obj)
        {
            NavigationManager.NavigateTo($"/tasks/update/{Todo.Guid}");
        }
    }
}
