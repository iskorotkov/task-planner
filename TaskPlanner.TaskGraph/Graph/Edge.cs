namespace TaskPlanner.TaskGraph.Graph
{
    public class Edge
    {
        public Edge(string source, string target)
        {
            Source = source;
            Target = target;
        }

        public string Source { get; }
        public string Target { get; }
    }
}
