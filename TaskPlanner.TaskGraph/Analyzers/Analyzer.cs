using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class Analyzer
    {
        private readonly Config _config;

        private PlacementGraph BuildPlacementGraph()
        {
            return null;
        }

        private RenderGraph BuildRenderGraph(PlacementGraph graph)
        {
            var renderGraph = new RenderGraph();

            foreach (var node in graph.Nodes)
            {
                var renderNode = new RenderNode
                {
                    Task = node.Task,
                    Position = new Position(
                        x: _config.LeftOffset + node.Position.X * (_config.NodeWidth + _config.HorizontalInterval),
                        y: _config.TopOffset + node.Position.Y * (_config.NodeHeight + _config.VerticalInterval)
                    ),
                    Dimensions = new Dimensions(
                        width: _config.NodeWidth,
                        height: _config.NodeHeight
                    )
                };
                renderGraph.Nodes.Add(renderNode);
            }

            foreach (var edge in graph.Edges)
            {
                var renderEdge = new RenderEdge
                {
                    From = new Position(
                        x: _config.LeftOffset
                            + edge.From.X * (_config.NodeWidth + _config.HorizontalInterval)
                            + _config.NodeWidth,
                        y: _config.TopOffset
                            + edge.From.Y * (_config.NodeHeight + _config.VerticalInterval)
                            + _config.NodeHeight / 2
                    ),
                    To = new Position(
                        x: _config.LeftOffset
                            + edge.To.X * (_config.NodeWidth + _config.HorizontalInterval),
                        y: _config.TopOffset
                            + edge.To.Y * (_config.NodeHeight + _config.VerticalInterval)
                            + _config.NodeHeight / 2
                    )
                };
                renderGraph.Edges.Add(renderEdge);
            }

            return renderGraph;
        }
    }
}
