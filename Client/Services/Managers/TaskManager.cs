using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Services.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly List<Todo> _tasks;

        public TaskManager()
        {
            _tasks = new List<Todo>
            {
                new Todo
                {
                    Title = "Task #1",
                    Description = "A simple task",
                    Author = "Ivan Korotkov"
                },
                new Todo
                {
                    Title = "Task #2",
                    Description = "A complex task",
                    Author = "Ivan Korotkov"
                },
                new Todo
                {
                    Title = "Task #3",
                    Description = "Just a reminder to do smth...",
                    Author = "Ivan Korotkov"
                },
                new Todo
                {
                    Title = "Task #4",
                    Description = "Grocery list:\n1. ...;\n2. ...",
                    Author = "Ivan Korotkov"
                }
            };
        }

        public Task<List<Todo>> Get()
        {
            return Task.FromResult(_tasks);
        }

        public Task Remove(Todo task)
        {
            _tasks.Remove(task);
            return Task.CompletedTask;
        }

        public Task Add(Todo task)
        {
            _tasks.Add(task);
            return Task.CompletedTask;
        }
    }
}
