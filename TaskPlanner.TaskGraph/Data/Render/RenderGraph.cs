using System;
using System.Collections.Generic;

namespace TaskPlanner.TaskGraph.Data.Render
{
    public class RenderGraph
    {
        public RenderGraph()
        {
            Nodes = new List<RenderNode>();
            Edges = new List<RenderEdge>();
        }

        public RenderGraph(List<RenderNode> nodes, List<RenderEdge> edges)
        {
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }

        public List<RenderNode> Nodes { get; }
        public List<RenderEdge> Edges { get; }

        public override bool Equals(object? obj)
        {
            return obj is RenderGraph graph &&
                   EqualityComparer<List<RenderNode>>.Default.Equals(Nodes, graph.Nodes) &&
                   EqualityComparer<List<RenderEdge>>.Default.Equals(Edges, graph.Edges);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nodes, Edges);
        }
    }
}
