using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.Storage;

namespace TaskPlanner.Shared.Services.Tasks
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskStorage _storage;

        public TaskManager(ITaskStorage storage)
        {
            _storage = storage;
        }

        public async Task<List<Todo>> GetAll()
        {
            var items = await _storage.GetAll().ConfigureAwait(false);
            return items.ToList();
        }

        public async Task Remove(Todo task)
        {
            await _storage.Delete(task).ConfigureAwait(false);
        }

        public async Task Add(Todo task)
        {
            await _storage.Add(task).ConfigureAwait(false);
        }

        public async Task Update(Todo task)
        {
            await _storage.Save(task).ConfigureAwait(false);
        }

        public async Task<Todo?> Find(string id)
        {
            return await _storage.Get(id).ConfigureAwait(false);
        }
    }
}
