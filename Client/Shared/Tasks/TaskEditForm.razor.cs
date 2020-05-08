using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Properties;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Data.Ui;

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
        [Parameter] public List<ActionButton>? Actions { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

#pragma warning disable 8618
        private EditContext EditContext { get; set; }
#pragma warning restore 8618

        private List<ActionButton> Sections { get; }

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
            Sections = new List<ActionButton>
            {
                new ActionButton(
                    "Add deadlines",
                    () => Model.Deadlines = new Deadlines(),
                    () => Model.Deadlines == null),
                new ActionButton(
                    "Add execution time",
                    () => Model.ExecutionTime = new ExecutionTime(),
                    () => Model.ExecutionTime == null),
                new ActionButton(
                    "Add iterations",
                    () => Model.Iterations = new Iterations(),
                    () => Model.Iterations == null),
                new ActionButton(
                    "Add metrics",
                    () => Model.Metrics = new Metrics(),
                    () => Model.Metrics == null)
            };
        }
    }
}
