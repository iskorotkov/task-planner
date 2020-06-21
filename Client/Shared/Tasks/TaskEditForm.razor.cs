using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Data.Ui;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskEditForm
    {
#pragma warning disable 8618
        [Parameter] public Todo Model { get; set; }
#pragma warning restore 8618

        [Parameter] public string? Title { get; set; }

        [Parameter] public List<ActionButton>? Actions { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
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
    }
}
