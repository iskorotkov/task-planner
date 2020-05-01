using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages
{
    public partial class Overview
    {
        [Inject] public ITaskManager TaskManager { get; set; } = null!;

        private List<Todo> _tasks = null!;

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.Get();
        }
    }
}
