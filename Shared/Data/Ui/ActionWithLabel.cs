using System;
using System.Threading.Tasks;

namespace Shared.Data.Ui
{
    public struct ActionWithLabel
    {
        public Func<Task> Task { get; }
        public string Label { get; }

        public ActionWithLabel(Func<Task> task, string label)
        {
            Task = task;
            Label = label;
        }
    }
}
