using System.Collections.Generic;
using TaskPlanner.Shared.Data.Tasks;
using System;

namespace TaskPlanner.TaskGraph.Data.Abstract
{
    public class AbstractNode
    {
        public AbstractNode(Todo task)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            References = new List<AbstractReference>();
        }

        public AbstractNode(Todo task, List<AbstractReference> references)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            References = references ?? throw new ArgumentNullException(nameof(references));
        }

        public Todo Task { get; }
        public List<AbstractReference> References { get; }

        public override bool Equals(object? obj)
        {
            return obj is AbstractNode node &&
                   EqualityComparer<Todo>.Default.Equals(Task, node.Task) &&
                   EqualityComparer<List<AbstractReference>>.Default.Equals(References, node.References);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Task, References);
        }
    }
}
