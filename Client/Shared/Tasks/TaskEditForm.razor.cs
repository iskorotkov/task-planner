using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Properties;
using TaskPlanner.Shared.Data.Sections;
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

        private List<Section> Sections { get; set; }

        protected override void OnParametersSet()
        {
            if (Model == null)
            {
                throw new ArgumentException("Model is null", nameof(Model));
            }

            EditContext = new EditContext(Model);
        }

        public TaskEditForm()
        {
            Sections = new List<Section>
            {
                new Section(
                    "Add deadlines",
                    () => Model.Deadlines = new Deadlines(),
                    () => Model.Deadlines == null),
                new Section(
                    "Add execution time",
                    () => Model.ExecutionTime = new ExecutionTime(),
                    () => Model.ExecutionTime == null),
                new Section(
                    "Add iterations",
                    () => Model.Iterations = new Iterations(),
                    () => Model.Iterations == null),
                new Section(
                    "Add metrics",
                    () => Model.Metrics = new Metrics(),
                    () => Model.Metrics == null)
            };
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
