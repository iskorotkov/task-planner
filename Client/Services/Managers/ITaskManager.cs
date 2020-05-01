using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Managers
{
    public interface ITaskManager
    {
        Task<List<Todo>> Get();
        Task Remove(Todo task);
        Task Add(Todo task);
    }
}
