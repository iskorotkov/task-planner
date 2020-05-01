using System.Collections.Generic;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages
{
    public partial class Overview
    {
        private readonly List<Todo> _tasks;

        private Todo? _selectedTask;

        public Overview()
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

        private void SetSelectedTask(Todo task)
        {
            _selectedTask = task;
        }
    }
}
