using System.Collections.Generic;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;

namespace Shared.Data.References
{
    public struct ReferencedTasks
    {
        public ReferenceType ReferenceType { get; }
        public List<Todo> Tasks { get; }

        public ReferencedTasks(ReferenceType referenceType, List<Todo> task)
        {
            ReferenceType = referenceType;
            Tasks = task ?? throw new System.ArgumentNullException(nameof(task));
        }
    }
}
