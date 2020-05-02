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
        public Todo Task { get; set; }
#pragma warning restore 8618

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
