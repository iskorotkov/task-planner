using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class RenderAnalyzer
    {
        public Task<RenderGraph> Analyze(PlacementGraph graph, GraphConfig config)
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
