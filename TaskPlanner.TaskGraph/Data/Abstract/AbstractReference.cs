using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.TaskGraph.Data.Abstract
{
    public class AbstractReference
    {
        public AbstractReference(AbstractNode node, ReferenceType type)
        {
            Node = node ?? throw new System.ArgumentNullException(nameof(node));
            Type = type;
        }

        public AbstractNode Node { get; }
        public ReferenceType Type { get; }
    }
}
