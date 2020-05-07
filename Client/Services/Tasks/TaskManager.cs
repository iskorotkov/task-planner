using System;
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
            var items = await _storage.GetAll();
            return _tasks ??= items.ToList();
        }

        public async Task Remove(Todo task)
        {
            _tasks?.Remove(task);
            await _storage.Delete(task);
        }

        public async Task Add(Todo task)
        {
            _tasks?.Add(task);
            await _storage.Save(task);
        }

        public async Task Update(Todo task)
        {
            await _storage.Save(task);
        }

        public async Task<Todo?> Find(string id)
        {
            var task = (await GetAll()).FirstOrDefault(x => x.Id == id);
            return task;
        }
    }
}
