using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Shared.Data.References
{
    public class Reference
    {
        public Todo Self { get; set; }
        public Todo Other { get; set; }
        public ReferenceType Type { get; set; }
    }
}
