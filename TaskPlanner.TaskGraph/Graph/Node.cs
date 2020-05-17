namespace TaskPlanner.TaskGraph.Graph
{
    public class Node
    {
        public Node(string id, string caption)
        {
            Id = id;
            Caption = caption;
        }

        public string Id { get; }
        public string Caption { get; }
    }
}
