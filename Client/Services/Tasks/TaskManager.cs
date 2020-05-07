using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Client.Services.Storage;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Tasks
{
    public class TaskManager : ITaskManager
    {
        private List<Todo>? _tasks;
        private readonly ITaskStorage _storage;

        public TaskManager(ITaskStorage storage)
        {
            _storage = storage;
        }

        public async Task<List<Todo>> GetAll()
        {
            return _tasks ??= await FetchTasks();
        }

        private async Task<List<Todo>> FetchTasks()
        {
            var items = await _storage.GetAll();
            return items.ToList();
        }

        public async Task Remove(Todo task)
        {
            await _storage.Delete(task);
            _tasks?.Remove(task);
        }

        public async Task Add(Todo task)
        {
            await _storage.Add(task);
            _tasks?.Add(task);
        }

        public async Task Update(Todo task)
        {
            await _storage.Save(task);
        }

        public async Task<Todo?> Find(string id)
        {
            var tasks = await GetAll();
            var task = tasks.FirstOrDefault(x => x.Id == id);
            return task;
        }
    }
}
