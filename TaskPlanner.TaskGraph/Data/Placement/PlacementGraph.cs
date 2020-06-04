using System;
using System.Collections.Generic;

namespace TaskPlanner.TaskGraph.Data.Placement
{
    public class PlacementGraph
    {
        public PlacementGraph()
        {
            Nodes = new List<PlacementNode>();
            Edges = new List<PlacementEdge>();
        }

        public PlacementGraph(List<PlacementNode> nodes, List<PlacementEdge> edges)
        {
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }

        public List<PlacementNode> Nodes { get; }
        public List<PlacementEdge> Edges { get; }

        public override bool Equals(object? obj)
        {
            return obj is PlacementGraph graph &&
                   EqualityComparer<List<PlacementNode>>.Default.Equals(Nodes, graph.Nodes) &&
                   EqualityComparer<List<PlacementEdge>>.Default.Equals(Edges, graph.Edges);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nodes, Edges);
        }
    }
}
