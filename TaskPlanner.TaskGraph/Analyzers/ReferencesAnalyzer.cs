using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class ReferencesAnalyzer
    {
        public async Task<RenderGraph> Analyze(List<Todo> tasks,
            PlacementConfig? placementConfig = null,
            RenderConfig? renderConfig = null)
        {
            var placementGraph = await BuildPlacementGraph(tasks, placementConfig ?? new PlacementConfig());
            return await BuildRenderGraph(placementGraph, renderConfig ?? new RenderConfig());
        }

        public Task<PlacementGraph> BuildPlacementGraph(List<Todo> tasks, PlacementConfig config)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var graph = new PlacementGraph();
            if (tasks.Count == 0)
            {
                return Task.FromResult(graph);
            }

            var queue = new Queue<(Todo Task, int Depth, int Count)>();
            queue.Enqueue((tasks[0], 0, 0));

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
                    if (config.ReferenceTypes?.Contains(reference.Type) == false)
                    {
                        continue;
                    }

                    var referencedTask = tasks.Find(x => x.Metadata.Id == reference.TargetId);
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

            return Task.FromResult(graph);
        }

        public Task<RenderGraph> BuildRenderGraph(PlacementGraph graph, RenderConfig config)
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
                if (edge.From.X <= edge.To.X)
                {
                    renderGraph.Edges.Add(new RenderEdge(
                        from: new Position(
                            x: config.LeftOffset
                                + edge.From.X * (config.NodeWidth + config.HorizontalInterval)
                                + config.NodeWidth,
                            y: config.TopOffset
                                + edge.From.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        to: new Position(
                            x: config.LeftOffset
                                + edge.To.X * (config.NodeWidth + config.HorizontalInterval),
                            y: config.TopOffset
                                + edge.To.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        type: edge.Type
                    ));
                }
                else if (edge.From.X > edge.To.X)
                {
                    renderGraph.Edges.Add(new RenderEdge(
                        from: new Position(
                            x: config.LeftOffset
                                + edge.From.X * (config.NodeWidth + config.HorizontalInterval),
                            y: config.TopOffset
                                + edge.From.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        to: new Position(
                            x: config.LeftOffset
                                + edge.To.X * (config.NodeWidth + config.HorizontalInterval)
                                + config.NodeWidth,
                            y: config.TopOffset
                                + edge.To.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        type: edge.Type
                    ));
                }
            }

            return Task.FromResult(renderGraph);
        }
    }
}
