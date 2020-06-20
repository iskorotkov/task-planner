using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Shared.Services.Storage
{
    public interface ITaskStorage
    {
        Task Save(Todo task);
        Task Delete(Todo task);
        Task Add(Todo task);
        Task<Todo> Get(string id);
        Task<List<Todo>> GetAll();
    }
}
