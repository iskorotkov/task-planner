using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class UpdateTask
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }

        [Parameter] public string GuidStr { get; set; }
        public Todo Task { get; set; }

        private Guid _guid;

        protected override async Task OnInitializedAsync()
        {
            _guid = Guid.Parse(GuidStr);
            Task = await TaskManager.Find(_guid);
            if (Task == null)
            {
                throw new ArgumentException(nameof(Task));
            }
        }

        private void Cancel(MouseEventArgs obj)
        {
            NavigationManager.NavigateTo("/overview");
        }

        private void Submit(MouseEventArgs obj)
        {
            NavigationManager.NavigateTo("/overview");
        }
    }
}
