namespace TaskPlanner.TaskGraph.Graph
{
    public class Edge
    {
        public Edge(string source, string target, string caption, string id = null)
        {
            Id = id;
            Source = source;
            Target = target;
            Caption = caption;
        }

        public string Id { get; }
        public string Source { get; }
        public string Target { get; }
        public string Caption { get; }
        
    }
}
