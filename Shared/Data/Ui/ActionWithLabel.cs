using System;
using System.Threading.Tasks;

namespace TaskPlanner.Shared.Data.Ui
{
    public class ActionWithLabel
    {
        public Func<Task> Task { get; }
        public string Label { get; }

        public ActionWithLabel(Func<Task> task, string label)
        {
            Task = task;
            Label = label;
        }
    }

    public class ActionWithLabel<T>
    {
        public Func<T, Task> Task { get; }
        public string Label { get; }

        public ActionWithLabel(Func<T, Task> task, string label)
        {
            Task = task;
            Label = label;
        }
    }
}
