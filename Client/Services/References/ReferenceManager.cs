using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.References
{
    public class ReferenceManager : IReferenceManager
    {
        public async Task AddChild(Todo child, Todo parent)
        {

        }

        public async Task AddDependency(Todo dependency, Todo dependant)
        {

        }

        public async Task AddAlternatives(List<Todo> alternatives)
        {

        }

        public async Task AddSimilar(List<Todo> similarTasks)
        {

        }

        public async Task AddTest(Todo test, Todo tested)
        {

        }
    }
}
