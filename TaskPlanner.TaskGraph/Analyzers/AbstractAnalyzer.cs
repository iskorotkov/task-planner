using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Abstract;
using TaskPlanner.TaskGraph.Data.Config;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class AbstractAnalyzer
    {
        public Task<AbstractGraph> Analyze(IEnumerable<Todo> tasks, GraphConfig config)
        {
            _ = tasks ?? throw new ArgumentNullException(nameof(tasks));

            if (!tasks.Any())
            {
                return Task.FromResult(new AbstractGraph());
            }

            var notVisited = tasks.ToHashSet();
            var nodes = new HashSet<AbstractNode>();
            var roots = new HashSet<AbstractNode>();
            while (notVisited.Count > 0)
            {
                var taskToAdd = notVisited.First();
                notVisited.Remove(taskToAdd);

                // Find already created node for this task
                var nodeToAdd = nodes.FirstOrDefault(x => x.Task == taskToAdd);

                // If it doesn't exist, create a new node
                if (nodeToAdd == null)
                {
                    nodeToAdd = new AbstractNode(taskToAdd);
                    nodes.Add(nodeToAdd);
                    roots.Add(nodeToAdd);
                }

                foreach (var reference in taskToAdd.References)
                {
                    if (!config.ReferenceTypes.HasFlag(reference.Type))
                    {
                        continue;
                    }

                    // Find node of referenced task
                    var referencedNode = nodes.FirstOrDefault(x => x.Task.Metadata.Id == reference.TargetId);

                    // Referenced task already assigned to the node
                    if (referencedNode != null)
                    {
                        // There is already a root with this task
                        if (roots.Contains(referencedNode))
                        {
                            // Remove node that is no longer a root
                            roots.Remove(referencedNode);

                            // Add root node if it wasn't already added
                            if (!roots.Contains(nodeToAdd))
                            {
                                roots.Add(nodeToAdd);
                            }
                        }
                    }
                    // Referenced task doesn't have a node yet
                    else
                    {
                        var referencedTask = tasks.FirstOrDefault(x => x.Metadata.Id == reference.TargetId);
                        if (referencedTask == null)
                        {
                            throw new ArgumentException("One of the provided tasks has unresolved reference to other task. Probably referenced task was deleted or wasn't created at all.");
                        }

                        referencedNode = new AbstractNode(referencedTask);
                    }

                    nodeToAdd.References.Add(new AbstractReference(referencedNode, reference.Type));
                }
            }

            // Remove graphs with only one node (there is no point in displaying them + they will clutter the graph)
            roots.RemoveWhere(x => x.References.Count == 0);
            return Task.FromResult(new AbstractGraph(roots.ToList()));
        }
    }
}
