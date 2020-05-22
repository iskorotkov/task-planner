using System;
using System.Collections.Generic;

namespace TaskPlanner.TaskGraph.Data.Abstract
{
    public class AbstractGraph
    {
        public AbstractGraph()
        {
            Roots = new List<AbstractNode>();
        }

        public AbstractGraph(List<AbstractNode> roots)
        {
            Roots = roots ?? throw new ArgumentNullException(nameof(roots));
        }

        public List<AbstractNode> Roots { get; }

        public override bool Equals(object? obj)
        {
            return obj is AbstractGraph graph &&
                   EqualityComparer<List<AbstractNode>>.Default.Equals(Roots, graph.Roots);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Roots);
        }
    }
}
