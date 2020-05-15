namespace TaskPlanner.Shared.Data.References
{
    public class Reference
    {
        public string TargetId { get; set; }
        public ReferenceType Type { get; set; }

        public Reference()
        {

        }

        public Reference(string targetId, ReferenceType type)
        {
            TargetId = targetId ?? throw new System.ArgumentNullException(nameof(targetId));
            Type = type;
        }
    }
}
