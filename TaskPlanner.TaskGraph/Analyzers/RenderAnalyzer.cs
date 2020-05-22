using System;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class RenderAnalyzer
    {
        private GraphConfig _config;

        public Task<RenderGraph> Analyze(PlacementGraph graph, GraphConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            var nodes = graph.Nodes.Select(CreateNode);
            var edges = graph.Edges.Select(CreateEdge);
            return Task.FromResult(new RenderGraph(nodes.ToList(), edges.ToList()));
        }

        private RenderEdge CreateEdge(PlacementEdge edge)
        {
            return new RenderEdge(
                from: new Position(
                    x: CornerPosition(edge.From.X, _config.Offsets.X, _config.Dimensions.Width, _config.Intervals.X)
                       + (edge.From.X <= edge.To.X ? _config.Dimensions.Width : 0),
                    y: CornerPosition(edge.From.Y, _config.Offsets.Y, _config.Dimensions.Height, _config.Intervals.Y)
                       + _config.Dimensions.Height / 2
                ),
                to: new Position(
                    x: CornerPosition(edge.To.X, _config.Offsets.X, _config.Dimensions.Width, _config.Intervals.X)
                       + (edge.From.X > edge.To.X ? _config.Dimensions.Width : 0),
                    y: CornerPosition(edge.To.Y, _config.Offsets.Y, _config.Dimensions.Height, _config.Intervals.Y)
                       + _config.Dimensions.Height / 2
                ),
                type: edge.Type
            );
        }

        private RenderNode CreateNode(PlacementNode node)
        {
            return new RenderNode(
                task: node.Task,
                position: new Position(
                    x: CornerPosition(node.Position.X, _config.Offsets.X, _config.Dimensions.Width,
                        _config.Intervals.X),
                    y: CornerPosition(node.Position.Y, _config.Offsets.Y, _config.Dimensions.Height,
                        _config.Intervals.Y)
                ),
                dimensions: new Dimensions(
                    width: _config.Dimensions.Width,
                    height: _config.Dimensions.Height
                )
            );
        }

        private int CornerPosition(int position, int offset, int sideLength, int interval)
        {
            return offset + position * (sideLength + interval);
        }
    }
}
