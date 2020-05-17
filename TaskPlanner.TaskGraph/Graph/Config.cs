using System.Collections.Generic;

namespace TaskPlanner.TaskGraph.Graph
{
    public class Config
    {
        public Graph DataSource { get; set; }
        public List<string> NodeTypes { get; } = new List<string>();
        public bool Cluster { get; set; }
        public List<string> ClusterColours { get; } = new List<string>();
        public bool DirectedEdges { get; set; }
    }
}
