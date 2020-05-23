using System.Threading.Tasks;
using TaskPlanner.Shared.Data.State;
using TaskPlanner.Shared.Services.Tasks;

namespace TaskPlanner.Client.Extensions
{
    public static class TaskManagerExtensions
    {
        public static async Task ApplyChanges(this ITaskManager manager, TaskEditingState state)
        {
            foreach (var task in state.AddedTasks)
            {
                await manager.Add(task).ConfigureAwait(false);
            }

            foreach (var task in state.ModifiedTasks)
            {
                await manager.Update(task).ConfigureAwait(false);
            }

            foreach (var task in state.RemovedTasks)
            {
                await manager.Remove(task).ConfigureAwait(false);
            }
        }
    }
}
