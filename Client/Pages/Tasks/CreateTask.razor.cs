using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Data.Ui;

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
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
#pragma warning restore 8618

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback<Todo> TaskCreated { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        // ReSharper disable once MemberCanBePrivate.Global
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        private Todo _task;
        private readonly List<ActionButton> _actions;

        public CreateTask()
        {
            _task = new Todo();
            _actions = new List<ActionButton>
            {
                new ActionButton("Save", Submit, () => true, "btn-success", "submit"),
                new ActionButton("Cancel", Cancel, () => true, "btn-secondary", "cancel")
            };
        }

        protected override async Task OnInitializedAsync()
        {
            // TODO: #11 Move task metadata initialization into separate service
            var state = await AuthenticationStateProvider
                .GetAuthenticationStateAsync()
                .ConfigureAwait(false);
            _task.Metadata.Id = Guid.NewGuid().ToString();
            _task.Metadata.Owner = state.User.FindFirst(claim => claim.Type == ClaimTypes.Email).Value;
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

        private Task Cancel()
        {
            NavigationManager.NavigateTo("/overview");
            return Task.CompletedTask;
        }
    }
}
