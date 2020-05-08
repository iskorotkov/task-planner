﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Data.Ui;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class UpdateTask
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public ITaskManager TaskManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        [Parameter] public string GuidStr { get; set; }
#pragma warning restore 8618

#pragma warning disable 8618
        private Todo _task;
#pragma warning restore 8618

        private readonly List<ActionButton> _actions;

        public UpdateTask()
        {
            _actions = new List<ActionButton>
            {
                new ActionButton("Save", Submit, () => true, "btn-success", "submit"),
                new ActionButton("Cancel", Cancel, () => true, "btn-secondary", "cancel"),
                new ActionButton("Delete", Delete, () => true, "btn-danger", "button")
            };
        }

        protected override async Task OnInitializedAsync()
        {
            _task = await TaskManager.Find(GuidStr).ConfigureAwait(false)
                ?? throw new ArgumentException(nameof(_task));
        }

        private Task Cancel()
        {
            NavigationManager.NavigateTo("/overview");
            return Task.CompletedTask;
        }

        private async Task Submit()
        {
            await TaskManager.Update(_task).ConfigureAwait(false);
            NavigationManager.NavigateTo("/overview");
        }

        private async Task Delete()
        {
            await TaskManager.Remove(_task).ConfigureAwait(false);
            NavigationManager.NavigateTo("/overview");
        }
    }
}
