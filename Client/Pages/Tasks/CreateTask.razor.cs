using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class CreateTask
    {
#pragma warning disable 8618
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public ITaskManager TaskManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
#pragma warning restore 8618

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback<Todo> TaskCreated { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        // ReSharper disable once MemberCanBePrivate.Global
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

#pragma warning disable 8618
        private Todo _task;
#pragma warning restore 8618

        protected override void OnInitialized()
        {
            _task = new Todo();
        }

        private async Task Submit()
        {
            await TaskManager.Add(_task!).ConfigureAwait(false);
            if (TaskCreated.HasDelegate)
            {
                await TaskCreated.InvokeAsync(_task!).ConfigureAwait(false);
            }

            NavigationManager.NavigateTo("/overview");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/overview");
        }
    }
}
