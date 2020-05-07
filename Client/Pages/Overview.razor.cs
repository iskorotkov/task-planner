using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages
{
    [Authorize]
    public partial class Overview
    {
#pragma warning disable 8618
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public ITaskManager TaskManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
#pragma warning restore 8618

#pragma warning disable 8618
        private List<Todo> _tasks;
#pragma warning restore 8618

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.GetAll().ConfigureAwait(false)
                     ?? throw new ArgumentException("List of tasks is null", nameof(_tasks));
        }

        private void CreateTask()
        {
            NavigationManager.NavigateTo("/tasks/create");
        }
    }
}
