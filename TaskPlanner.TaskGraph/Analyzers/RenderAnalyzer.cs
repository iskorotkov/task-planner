using System;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;
using TaskPlanner.TaskGraph.Layout;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class RenderAnalyzer
    {
        private GraphConfig _config;
        private readonly LayoutBuilder _layoutBuilder;

        public RenderAnalyzer(LayoutBuilder layoutBuilder)
        {
            _layoutBuilder = layoutBuilder ?? throw new ArgumentNullException(nameof(layoutBuilder));
        }

        public Task<RenderGraph> Analyze(PlacementGraph graph, GraphConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            var nodes = graph.Nodes.Select(CreateNode);
            var edges = graph.Edges.Select(CreateEdge);
            return Task.FromResult(new RenderGraph(nodes.ToList(), edges.ToList()));
        }

        private RenderEdge CreateEdge(PlacementEdge edge)
        {
            var (from, to) = RenderPosition(edge);
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

            from += verticalOffset;
            to += verticalOffset;
            var labelPosition = (from + to) / 2 + _config.EdgeLabel.Offset;
            return new RenderEdge(
                from: from,
                to: to,
                type: edge.Type,
                new RenderElement(labelPosition, _config.EdgeLabel.MaxLetters)
            );
        }

        private RenderNode CreateNode(PlacementNode node)
        {
            var position = RenderPosition(node);
            var elements = _layoutBuilder.Build(position, _config);
            return new RenderNode(
                task: node.Task,
                position: position,
                dimensions: _config.Dimensions,
                elements: elements.ToList()
            );
        }

        private int RenderPosition(int position, int offset, int sideLength, int interval)
        {
            return offset + position * (sideLength + interval);
        }

        private Position RenderPosition(Position position, Position offset, Dimensions dimensions, Position intervals)
        {
            var x = RenderPosition(position.X, offset.X, dimensions.Width, intervals.X);
            var y = RenderPosition(position.Y, offset.Y, dimensions.Height, intervals.Y);
            return new Position(x, y);
        }

        private Position RenderPosition(PlacementNode node)
        {
            return RenderPosition(node.Position, _config.Offsets, _config.Dimensions, _config.Intervals);
        }

        private (Position From, Position To) RenderPosition(PlacementEdge edge)
        {
            var from = RenderPosition(edge.From, _config.Offsets, _config.Dimensions, _config.Intervals);
            var to = RenderPosition(edge.To, _config.Offsets, _config.Dimensions, _config.Intervals);
            return (from, to);
        }
    }
}
