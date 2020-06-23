namespace TaskPlanner.Shared.Data.Sections
{
    public class Metric : OptionalSection
    {
        public string Title { get; set; } = "Metric";
        public int Value { get; set; }
    }
}
