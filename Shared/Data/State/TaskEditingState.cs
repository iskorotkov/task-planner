using System.Collections.Generic;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Shared.Data.State
{
    public class TaskEditingState
    {
        public List<Todo> AddedTasks { get; } = new List<Todo>();
        public List<Todo> ModifiedTasks { get; } = new List<Todo>();
        public List<Todo> RemovedTasks { get; } = new List<Todo>();
    }
}
