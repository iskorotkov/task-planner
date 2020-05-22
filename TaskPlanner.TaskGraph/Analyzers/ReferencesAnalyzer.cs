using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Abstract;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class ReferencesAnalyzer
    {
        public async Task<RenderGraph> Analyze(IEnumerable<Todo> tasks, GraphConfig? config = null)
        {
            config ??= new GraphConfig();
            var placementGraph = await BuildPlacementGraph(tasks, config);
            return await BuildRenderGraph(placementGraph, config);
        }

        public Task<AbstractGraph> BuildAbstractGraph(IEnumerable<Todo> tasks, GraphConfig config)
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

                    nodeToAdd.References.Add(referencedNode);
                }
            }

            // Remove graphs with only one node (there is no point in displaying them + they will clutter the graph)
            roots.RemoveWhere(x => x.References.Count == 0);
            return Task.FromResult(new AbstractGraph(roots.ToList()));
        }

        public Task<PlacementGraph> BuildPlacementGraph(IEnumerable<Todo> tasks, GraphConfig config)
        {
            _ = tasks ?? throw new ArgumentNullException(nameof(tasks));

            var graph = new PlacementGraph();
            if (!tasks.Any())
            {
                return Task.FromResult(graph);
            }

            var queue = new Queue<(Todo Task, int Depth, int Count)>();
            queue.Enqueue((tasks.First(), 0, 0));

            var visited = new HashSet<Todo>();
            while (queue.Count > 0)
            {
                var (task, depth, count) = queue.Dequeue();
                visited.Add(task);
                graph.Nodes.Add(new PlacementNode(
                    task: task,
                    position: new Position(depth, count)
                ));

                var referencedDepth = depth + 1;
                var referencedCount = 0;
                foreach (var reference in task.References)
                {
                    if (!config.ReferenceTypes.HasFlag(reference.Type))
                    {
                        continue;
                    }

                    var referencedTask = tasks.FirstOrDefault(x => x.Metadata.Id == reference.TargetId);
                    if (referencedTask == null)
                    {
                        throw new ArgumentException("One of the provided tasks has unresolved reference to other task. Probably referenced task was deleted or wasn't created at all.");
                    }

                    if (visited.Contains(referencedTask))
                    {
                        var placedNode = graph.Nodes.Find(x => x.Task == referencedTask);
                        graph.Edges.Add(new PlacementEdge(
                            from: new Position(depth, count),
                            to: placedNode.Position,
                            type: reference.Type
                        ));
                    }
                    else
                    {
                        graph.Edges.Add(new PlacementEdge(
                            from: new Position(depth, count),
                            to: new Position(referencedDepth, referencedCount),
                            type: reference.Type
                        ));

                        queue.Enqueue((referencedTask, referencedDepth, referencedCount));
                        referencedCount++;
                    }
                }
            }

            // If there are no edges, we shouldn't display any node
            if (graph.Edges.Count == 0)
            {
                graph.Nodes.Clear();
            }
            return Task.FromResult(graph);
        }

        public Task<RenderGraph> BuildRenderGraph(PlacementGraph graph, GraphConfig config)
        {
            var renderGraph = new RenderGraph();

            foreach (var node in graph.Nodes)
            {
                var renderNode = new RenderNode(
                    task: node.Task,
                    position: new Position(
                        x: config.LeftOffset + node.Position.X * (config.NodeWidth + config.HorizontalInterval),
                        y: config.TopOffset + node.Position.Y * (config.NodeHeight + config.VerticalInterval)
                    ),
                    dimensions: new Dimensions(
                        width: config.NodeWidth,
                        height: config.NodeHeight
                    )
                );
                renderGraph.Nodes.Add(renderNode);
            }

            foreach (var edge in graph.Edges)
            {
                renderGraph.Edges.Add(new RenderEdge(
                    from: new Position(
                        x: config.LeftOffset
                            + edge.From.X * (config.NodeWidth + config.HorizontalInterval)
                            + (edge.From.X <= edge.To.X ? config.NodeWidth : 0),
                        y: config.TopOffset
                            + edge.From.Y * (config.NodeHeight + config.VerticalInterval)
                            + config.NodeHeight / 2
                    ),
                    to: new Position(
                        x: config.LeftOffset
                            + edge.To.X * (config.NodeWidth + config.HorizontalInterval)
                            + (edge.From.X > edge.To.X ? config.NodeWidth : 0),
                        y: config.TopOffset
                            + edge.To.Y * (config.NodeHeight + config.VerticalInterval)
                            + config.NodeHeight / 2
                    ),
                    type: edge.Type
                ));
            }

            return Task.FromResult(renderGraph);
        }
    }
}
