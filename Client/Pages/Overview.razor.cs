using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages
{
    public partial class Overview
    {
#pragma warning disable 8618
        // ReSharper disable once MemberCanBePrivate.Global
        [Inject] public ITaskManager TaskManager { get; set; } = null!;

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
#pragma warning restore 8618

        private List<Todo> _tasks = null!;

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.Get().ConfigureAwait(false);
        }

        private void CreateTask()
        {
            NavigationManager.NavigateTo("/tasks/create");
        }
    }
}
