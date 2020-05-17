using System.Collections.Generic;

namespace TaskPlanner.TaskGraph.Graph
{
    public class Tree
    {
        public List<Node> Nodes { get; } = new List<Node>();
        public List<Edge> Edges { get; } = new List<Edge>();
    }
}
