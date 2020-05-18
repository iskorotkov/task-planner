using System;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class Analyzer
    {
        public RenderGraph Analyze(List<Todo> tasks, Config config)
        {
            var placementGraph = BuildPlacementGraph(tasks);
            return BuildRenderGraph(placementGraph, config);
        }

        public PlacementGraph BuildPlacementGraph(List<Todo> tasks)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var graph = new PlacementGraph();
            if (tasks.Count == 0)
            {
                return graph;
            }

            var queue = new Queue<(Todo Task, int Depth, int Count)>();
            queue.Enqueue((tasks[0], 0, 0));

            var visited = new HashSet<Todo>();
            while (queue.Count > 0)
            {
                var (task, depth, count) = queue.Dequeue();
                visited.Add(task);
                graph.Nodes.Add(new PlacementNode
                {
                    Task = task,
                    Position = new Position(depth, count)
                });

                var referencedDepth = depth + 1;
                var referencedCount = 0;
                foreach (var reference in task.References)
                {
                    var referencedTask = tasks.Find(x => x.Metadata.Id == reference.TargetId);
                    if (referencedTask == null)
                    {
                        throw new ArgumentException("One of the provided tasks has unresolved reference to other task. Probably referenced task was deleted or wasn't created at all.");
                    }

                    if (visited.Contains(referencedTask))
                    {
                        var placedNode = graph.Nodes.Find(x => x.Task == referencedTask);
                        graph.Edges.Add(new PlacementEdge
                        {
                            From = new Position(depth, count),
                            To = placedNode.Position
                        });
                    }
                    else
                    {
                        graph.Edges.Add(new PlacementEdge
                        {
                            From = new Position(depth, count),
                            To = new Position(referencedDepth, referencedCount)
                        });

                        queue.Enqueue((referencedTask, referencedDepth, referencedCount));
                        referencedCount++;
                    }
                }
            }

            return graph;
        }

        public RenderGraph BuildRenderGraph(PlacementGraph graph, Config config)
        {
            var renderGraph = new RenderGraph();

            foreach (var node in graph.Nodes)
            {
                var renderNode = new RenderNode
                {
                    Task = node.Task,
                    Position = new Position(
                        x: config.LeftOffset + node.Position.X * (config.NodeWidth + config.HorizontalInterval),
                        y: config.TopOffset + node.Position.Y * (config.NodeHeight + config.VerticalInterval)
                    ),
                    Dimensions = new Dimensions(
                        width: config.NodeWidth,
                        height: config.NodeHeight
                    )
                };
                renderGraph.Nodes.Add(renderNode);
            }

            foreach (var edge in graph.Edges)
            {
                if (edge.From.X < edge.To.X)
                {
                    renderGraph.Edges.Add(new RenderEdge
                    {
                        From = new Position(
                            x: config.LeftOffset
                                + edge.From.X * (config.NodeWidth + config.HorizontalInterval)
                                + config.NodeWidth,
                            y: config.TopOffset
                                + edge.From.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        To = new Position(
                            x: config.LeftOffset
                                + edge.To.X * (config.NodeWidth + config.HorizontalInterval),
                            y: config.TopOffset
                                + edge.To.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        )
                    });
                }
                else if (edge.From.X > edge.To.X)
                {
                    renderGraph.Edges.Add(new RenderEdge
                    {
                        From = new Position(
                            x: config.LeftOffset
                                + edge.From.X * (config.NodeWidth + config.HorizontalInterval),
                            y: config.TopOffset
                                + edge.From.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        ),
                        To = new Position(
                            x: config.LeftOffset
                                + edge.To.X * (config.NodeWidth + config.HorizontalInterval)
                                + config.NodeWidth,
                            y: config.TopOffset
                                + edge.To.Y * (config.NodeHeight + config.VerticalInterval)
                                + config.NodeHeight / 2
                        )
                    });
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return renderGraph;
        }
    }
}
