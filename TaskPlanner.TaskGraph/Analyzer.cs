using System.Collections.Generic;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Graph;

namespace TaskPlanner.TaskGraph
{
    public class Analyzer
    {
        public Tree Analyze(List<Todo> tasks)
        {
            var tree = new Tree();

            foreach (var task in tasks)
            {
                var node = new Node(task.Metadata.Id, task.Content.Title);
                tree.Nodes.Add(node);

                foreach (var reference in task.References)
                {
                    var edge = new Edge(reference.TargetId,
                        reference.Type.ToString(),
                        task.Metadata.Id);
                    tree.Edges.Add(edge);
                }
            }

            return tree;
        }
    }
}
