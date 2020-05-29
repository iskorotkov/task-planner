using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Extensions;
using TaskPlanner.Shared.Services.Formatters;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.Client.Services.Canvas
{
    public class CanvasPainter
    {
        private BECanvasComponent? _canvas;
        private Canvas2DContext? _context;
        private PainterConfig _config;
        private readonly NodeTextFormatter _nodeTextFormatter;

        public CanvasPainter(NodeTextFormatter nodeTextFormatter)
        {
            _nodeTextFormatter = nodeTextFormatter ?? throw new ArgumentNullException(nameof(nodeTextFormatter));
        }

        public async Task Init(BECanvasComponent canvas, PainterConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _ = canvas ?? throw new ArgumentNullException(nameof(canvas));

            if (_canvas != canvas)
            {
                _canvas = canvas;
                _context = await _canvas.CreateCanvas2DAsync();
            }
            else
            {
                await _context.ClearRectAsync(0, 0, _canvas.Width, _canvas.Height);
            }
        }

        public async Task DrawGraph(RenderGraph graph)
        {
            EnsureInitialized();
            foreach (var node in graph.Nodes)
            {
                await DrawNode(node);
            }

            foreach (var edge in graph.Edges)
            {
                await DrawEdge(edge);
            }
        }

        private async Task DrawNode(RenderNode node)
        {
            await _context.StrokeRectAsync(node.Position.X, node.Position.Y,
                node.Dimensions.Width, node.Dimensions.Height);
            await DrawNodeComponents(node);
        }

        private async Task DrawNodeComponents(RenderNode node)
        {
            var index = 0;
            RenderElement NextElement() => node.Elements[index++];

            await DrawElement(NextElement(), node.Task.Content.Title ?? "?");
            await DrawElement(NextElement(), node.Task.Content.Description ?? "?");

            if (node.Task.Participants.Author is { } author)
            {
                await DrawRect(NextElement(), "green");
                await DrawElement(NextElement(), author);
            }

            if (node.Task.ExecutionTime is { } time)
            {
                await DrawRect(NextElement(), "blue");
                var estimated = time.EstimatedTime?.ToShortString() ?? "?";
                var spent = time.TimeSpent?.ToShortString() ?? "?";
                await DrawElement(NextElement(), $"Time: {estimated} / {spent}");
            }

            if (node.Task.Deadlines is { } deadlines)
            {
                await DrawRect(NextElement(), "red");
                var hd = deadlines.HardDeadline?.ToString() ?? "?";
                var sd = deadlines.SoftDeadline.ToString() ?? "?";
                await DrawElement(NextElement(), $"Deadlines: {sd} : {hd}");
            }

            if (node.Task.Iterations is { } iterations)
            {
                await DrawRect(NextElement(), "yellow");
                var perIteration = iterations.TimePerIteration?.ToShortString() ?? "?";
                await DrawElement(NextElement(), $"Iterations: {iterations.Executed} / {iterations.Required} x{perIteration}");
            }

            if (node.Task.Metrics is { } metrics)
            {
                await DrawRect(NextElement(), "purple");
                await DrawElement(NextElement(), $"Metrics: C={metrics.Complexity}, I={metrics.Importance}");
            }

            if (node.Task.References.Count > 0)
            {
                await DrawRect(NextElement(), "grey");
                await DrawElement(NextElement(), $"References: {node.Task.References.Count}");
            }
        }

        private async Task DrawElement(RenderElement element, string text)
        {
            var position = element.Position + new Position(0, element.Dimensions.Height);
            text = _nodeTextFormatter.ClampText(text, element.Dimensions.Width);
            await _context.FillTextAsync(text, position.X, position.Y);
        }

        private async Task DrawRect(RenderElement element, string style)
        {
            var oldStyle = _context.FillStyle;
            await _context.SetFillStyleAsync(style);
            await _context.FillRectAsync(element.Position.X, element.Position.Y,
                element.Dimensions.Width, element.Dimensions.Height);
            await _context.SetFillStyleAsync(oldStyle);
        }

        private async Task DrawEdge(RenderEdge edge)
        {
            var types = edge.Types & _config.Types;
            if (types == 0)
            {
                return;
            }

            await DrawEdgeLine(edge);
            await DrawEdgeLabel(edge, types);
        }

        private async Task DrawEdgeLabel(RenderEdge edge, ReferenceType types)
        {
            var typeLabels = types.ToString().Split(", ");
            var position = edge.Label.Position + new Position(0, edge.Label.Dimensions.Height);
            var label = typeLabels[0];
            if (typeLabels.Length == 1)
            {
                label = _nodeTextFormatter.ClampText(label, edge.Label.Dimensions.Width);
            }
            else
            {
                var suffix = $" +{typeLabels.Length - 1}";
                label = _nodeTextFormatter.ClampText(label, edge.Label.Dimensions.Width - suffix.Length);
                label += suffix;
            }
            await _context.FillTextAsync(label, position.X, position.Y);
        }

        private async Task DrawEdgeLine(RenderEdge edge)
        {
            await _context.BeginPathAsync();
            await _context.MoveToAsync(edge.From.X, edge.From.Y);
            await _context.LineToAsync(edge.To.X, edge.To.Y);
            await _context.ClosePathAsync();
            await _context.StrokeAsync();
        }

        private void EnsureInitialized()
        {
            if (_context == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CanvasPainter)} wasn't initialized yet. Call {nameof(Init)} before using it.");
            }
        }
    }
}
