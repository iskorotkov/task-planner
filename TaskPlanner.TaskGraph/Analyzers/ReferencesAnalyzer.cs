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
            var abstractGraph = await BuildAbstractGraph(tasks, config);
            var placementGraph = await BuildPlacementGraph(abstractGraph, config);
            var renderGraph = await BuildRenderGraph(placementGraph, config);
            return renderGraph;
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

                    nodeToAdd.References.Add(new AbstractReference(referencedNode, reference.Type));
                }
            }

            // Remove graphs with only one node (there is no point in displaying them + they will clutter the graph)
            roots.RemoveWhere(x => x.References.Count == 0);
            return Task.FromResult(new AbstractGraph(roots.ToList()));
        }

        public Task<PlacementGraph> BuildPlacementGraph(AbstractGraph abstractGraph, GraphConfig config)
        {
            _ = abstractGraph ?? throw new ArgumentNullException(nameof(abstractGraph));

            if (abstractGraph.Roots.Count == 0)
            {
                return Task.FromResult(new PlacementGraph());
            }

            var graph = new PlacementGraph();
            var placedTasks = new HashSet<Todo>();
            var subgraphRow = 0;
            foreach (var root in abstractGraph.Roots)
            {
                PlaceSubgraph(graph, placedTasks, root, subgraphRow);
            }

            return Task.FromResult(graph);
        }

        private void PlaceSubgraph(PlacementGraph graph, HashSet<Todo> placedTasks, AbstractNode root, int subgraphRow)
        {
            var nodesQueue = new Queue<(AbstractNode Node, int Row, int Column)>();
            nodesQueue.Enqueue((root, subgraphRow, 0));
            while (nodesQueue.Count > 0)
            {
                var (nextNode, row, column) = nodesQueue.Dequeue();
                var placementNode = new PlacementNode(nextNode.Task, new Position(column, row));
                graph.Nodes.Add(placementNode);
                placedTasks.Add(nextNode.Task);

                var referenceRow = row;
                var referenceColumn = column + 1;
                foreach (var reference in nextNode.References)
                {
                    if (placedTasks.Contains(reference.Node.Task))
                    {
                        var placedNode = graph.Nodes.Find(x => x.Task == reference.Node.Task);
                        graph.Edges.Add(new PlacementEdge(
                            from: new Position(column, row),
                            to: placedNode.Position,
                            type: reference.Type
                        ));
                    }
                    else
                    {
                        graph.Edges.Add(new PlacementEdge(
                            from: new Position(column, row),
                            to: new Position(referenceColumn, referenceRow),
                            type: reference.Type
                        ));

                        nodesQueue.Enqueue((reference.Node, referenceRow, referenceColumn));
                        referenceRow++;
                    }
                }
            }
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
