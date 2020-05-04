using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskEditForm
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
#pragma warning disable 8618
        [Parameter] public Todo Model { get; set; }
#pragma warning restore 8618

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public string? Title { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public RenderFragment<EditContext>? AdditionalButtons { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback<EditContext> Cancel { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback<EditContext> Submit { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

#pragma warning disable 8618
        private EditContext EditContext { get; set; }
#pragma warning restore 8618

        protected override void OnParametersSet()
        {
            if (Model == null)
            {
                throw new ArgumentException("Model is null", nameof(Model));
            }

            EditContext = new EditContext(Model);
        }

        private async Task OnCancel()
        {
            if (Cancel.HasDelegate)
            {
                await Cancel.InvokeAsync(EditContext).ConfigureAwait(false);
            }
        }

        private async Task OnSubmit()
        {
            if (Submit.HasDelegate)
            {
                await Submit.InvokeAsync(EditContext).ConfigureAwait(false);
            }
        }
    }
}
