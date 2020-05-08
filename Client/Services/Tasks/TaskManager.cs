using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TaskPlanner.Client.Services.Storage;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Tasks
{
    public class TaskManager : ITaskManager
    {
        private List<Todo>? _tasks;
        private readonly ITaskStorage _storage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public TaskManager(ITaskStorage storage, AuthenticationStateProvider authenticationStateProvider)
        {
            _storage = storage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<Todo>> GetAll()
        {
            return _tasks ??= await FetchTasks().ConfigureAwait(false);
        }

        private async Task<List<Todo>> FetchTasks()
        {
            var items = await _storage.GetAll().ConfigureAwait(false);
            return items.ToList();
        }

        public async Task Remove(Todo task)
        {
            await _storage.Delete(task).ConfigureAwait(false);
            _tasks?.Remove(task);
        }

        public async Task Add(Todo task)
        {
            var state = await _authenticationStateProvider
                .GetAuthenticationStateAsync()
                .ConfigureAwait(false);
            task.Metadata.Id = Guid.NewGuid().ToString();
            task.Metadata.Owner = state.User.FindFirst(claim => claim.Type == ClaimTypes.Email).Value;

            await _storage.Add(task).ConfigureAwait(false);
            _tasks?.Add(task);
        }

        public async Task Update(Todo task)
        {
            await _storage.Save(task).ConfigureAwait(false);
        }

        public async Task<Todo?> Find(string id)
        {
            var tasks = await GetAll().ConfigureAwait(false);
            return tasks.Find(x => x.Metadata.Id == id);
        }
    }
}
