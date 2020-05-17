namespace TaskPlanner.TaskGraph.Graph
{
    public class Node
    {
        public Node(string id, string name, string nodeType, int cluster)
        {
            Id = id;
            Name = name;
            NodeType = nodeType;
            Cluster = cluster;
        }

        public string Id { get; }
        public string Name { get; }
        public string NodeType { get; }
        public int Cluster { get; }
    }
}
