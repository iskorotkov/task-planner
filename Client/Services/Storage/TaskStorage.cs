using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
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

        public async Task<string> Save(Todo task)
        {
            var path = $"tasks/{task.Id}";
            return await _jsRuntime.InvokeAsync<string>("firestore.save", path, task);
        }

        public async Task Delete(Todo task)
        {
            var path = $"tasks/{task.Id}";
            await _jsRuntime.InvokeVoidAsync("firestore.delete", path);
        }

        public async Task<Todo> Get(string id)
        {
            var path = $"tasks/{id}";
            return await _jsRuntime.InvokeAsync<Todo>("firestore.getDoc", path);
        }

        public async Task<IEnumerable<Todo>> GetAll()
        {
            const string path = "tasks";
            return await _jsRuntime.InvokeAsync<IEnumerable<Todo>>("firestore.getCollection", path);
        }
    }
}
