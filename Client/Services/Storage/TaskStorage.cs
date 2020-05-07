using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Storage
{
    public class TaskStorage : ITaskStorage
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DotNetObjectReference<TaskStorage> _reference;

        public TaskStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _reference = DotNetObjectReference.Create(this);
        }

        public void Dispose()
        {
            _reference.Dispose();
        }

        public async Task Save(Todo task)
        {
            var path = $"tasks/{task.Id}";
            await _jsRuntime.InvokeVoidAsync("firestore.save", path, task);
        }

        public async Task Delete(Todo task)
        {
            var path = $"tasks/{task.Id}";
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
            var items = await _jsRuntime.InvokeAsync<IEnumerable<Todo>>("firestore.getCollection", tasksPath)
                .ConfigureAwait(false);
            return items.ToList();
        }

        public async Task Add(Todo task)
        {
            task.Id = Guid.NewGuid().ToString();
            await Save(task).ConfigureAwait(false);
        }
    }
}
