﻿using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class UpdateTask
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public ITaskManager TaskManager { get; set; }
#pragma warning restore 8618

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
#pragma warning disable 8618
        [Parameter] public string GuidStr { get; set; }
#pragma warning restore 8618

#pragma warning disable 8618
        private Todo _task;
#pragma warning restore 8618

        protected override async Task OnInitializedAsync()
        {
            _task = await TaskManager.Find(GuidStr).ConfigureAwait(false)
                ?? throw new ArgumentException(nameof(_task));
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/overview");
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
