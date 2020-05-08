using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Conditions;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Storage
{
    public class TaskStorage : ITaskStorage
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DotNetObjectReference<TaskStorage> _reference;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public TaskStorage(IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider)
        {
            _jsRuntime = jsRuntime;
            _reference = DotNetObjectReference.Create(this);
            _authenticationStateProvider = authenticationStateProvider;
        }

        public void Dispose()
        {
            _reference.Dispose();
        }

        public async Task Save(Todo task)
        {
            var path = $"tasks/{task.Metadata.Id}";
            await _jsRuntime.InvokeVoidAsync("firestore.save", path, task);
        }

        public async Task Delete(Todo task)
        {
            var path = $"tasks/{task.Metadata.Id}";
            await _jsRuntime.InvokeVoidAsync("firestore.delete", path);
        }

        public async Task<Todo> Get(string id)
        {
            var path = $"tasks/{id}";
            return await _jsRuntime.InvokeAsync<Todo>("firestore.getDoc", path)
                .ConfigureAwait(false);
        }

        public async Task<List<Todo>> GetAll()
        {
            return await FetchTasks().ConfigureAwait(false);
        }

        private async Task<List<Todo>> FetchTasks()
        {
            const string tasksPath = "tasks";
            var conditions = await GetConditionsForFetch().ConfigureAwait(false);
            var items = await _jsRuntime
                .InvokeAsync<IEnumerable<Todo>>("firestore.getCollection", tasksPath, conditions)
                .ConfigureAwait(false);
            return items.ToList();
        }

        private async Task<Condition[]> GetConditionsForFetch()
        {
            var state = await _authenticationStateProvider
                            .GetAuthenticationStateAsync()
                            .ConfigureAwait(false);
            var email = state.User.FindFirst(claim => claim.Type == ClaimTypes.Email);
            return new[]
            {
                new Condition("metadata.owner", "==", email?.Value ?? "user@mail.com")
            };
        }

        public async Task Add(Todo task)
        {
            await Save(task).ConfigureAwait(false);
        }
    }
}
