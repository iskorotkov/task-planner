using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Storage
{
    public interface ITaskStorage
    {
        Task<string> Save(Todo task);
        Task Delete(Todo task);
        Task<Todo> Get(string id);
        Task<IEnumerable<Todo>> GetAll();
    }
}
