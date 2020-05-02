using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages
{
    public partial class Overview
    {
        [Inject] public ITaskManager TaskManager { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; }

        private List<Todo> _tasks = null!;

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.Get();
        }

        private void CreateTask(MouseEventArgs obj)
        {
            NavigationManager.NavigateTo("/tasks/create");
        }
    }
}
