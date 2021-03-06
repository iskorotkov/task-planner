﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskCard
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }
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

        private void CardClicked()
        {
            NavigationManager.NavigateTo($"/tasks/update/{Todo.Metadata.Id}");
        }

        private async Task IncrementIterations()
        {
            if (Todo.Iterations == null)
            {
                throw new InvalidOperationException(
                    "Can't increment number of iterations as iterations are not added to the task.");
            }

            Todo.Iterations.Executed++;
            await TaskManager.Update(Todo).ConfigureAwait(false);
        }
    }
}
