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
