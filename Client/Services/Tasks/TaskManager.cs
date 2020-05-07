using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Tasks
{
    public class TaskManager : ITaskManager
    {
        private List<Todo>? _tasks;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public TaskManager(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<Todo>> GetAll()
        {
            return new List<Todo>();
        }

        public async Task Remove(Todo task)
        {
            _tasks?.Remove(task);
        }

        public async Task Add(Todo task)
        {
            _tasks?.Add(task);
        }

        public async Task Update(Todo task)
        {
        }

        public Task<Todo?> Find(Guid guid)
        {
            var task = _tasks?.FirstOrDefault(x => x.Guid == guid);
            return Task.FromResult(task);
        }
    }
}
