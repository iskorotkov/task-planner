using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Tasks
{
    public interface ITaskManager
    {
        Task<List<Todo>> GetAll();
        Task Remove(Todo task);
        Task Add(Todo task);
        Task<Todo?> Find(string id);
        Task Update(Todo task);
    }
}
