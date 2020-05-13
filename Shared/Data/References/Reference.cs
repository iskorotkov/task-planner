using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Shared.Data.References
{
    public class Reference
    {
        public Todo Target { get; set; }
        public ReferenceType Type { get; set; }

        public Reference()
        {

        }

        public Reference(Todo target, ReferenceType type)
        {
            Target = target;
            Type = type;
        }
    }
}
