using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.References
{
    public interface IReferenceManager
    {
        Task AddAlternatives(List<Todo> alternatives);
        Task AddChild(Todo child, Todo parent);
        Task AddDependency(Todo dependency, Todo dependant);
        Task AddSimilar(List<Todo> similarTasks);
        Task AddTest(Todo test, Todo tested);
    }
}