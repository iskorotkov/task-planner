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
            var (from, to) = Position(edge);
            var verticalOffset = new Position(0, _config.Dimensions.Height / 2);
            var horizontalOffset = new Position(_config.Dimensions.Width, 0);
            if (edge.From.X <= edge.To.X)
            {
                from += horizontalOffset;
            }
            else
            {
                to += horizontalOffset;
            }
            return new RenderEdge(
                from: from + verticalOffset,
                to: to + verticalOffset,
                type: edge.Type
            );
        }

        private RenderNode CreateNode(PlacementNode node)
        {
            return new RenderNode(
                task: node.Task,
                position: Position(node),
                dimensions: _config.Dimensions
            );
        }

        private int Position(int position, int offset, int sideLength, int interval)
        {
            return offset + position * (sideLength + interval);
        }

        private Position Position(Position position, Position offset, Dimensions dimensions, Position intervals)
        {
            var x = Position(position.X, offset.X, dimensions.Width, intervals.X);
            var y = Position(position.Y, offset.Y, dimensions.Height, intervals.Y);
            return new Position(x, y);
        }

        private Position Position(PlacementNode node)
        {
            return Position(node.Position, _config.Offsets, _config.Dimensions, _config.Intervals);
        }

        private (Position From, Position To) Position(PlacementEdge edge)
        {
            var from = Position(edge.From, _config.Offsets, _config.Dimensions, _config.Intervals);
            var to = Position(edge.To, _config.Offsets, _config.Dimensions, _config.Intervals);
            return (from, to);
        }
    }
}
