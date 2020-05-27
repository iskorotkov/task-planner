using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
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

            await DrawElement(node.Task.Content.Title ?? "?", NextElement());
            await DrawElement(node.Task.Content.Description ?? "?", NextElement());

            if (node.Task.Participants.Author is { } author)
            {
                await DrawElement(author, NextElement());
            }

            if (node.Task.ExecutionTime is { } time)
            {
                var estimated = time.EstimatedTime?.ToShortString() ?? "?";
                var spent = time.TimeSpent?.ToShortString() ?? "?";
                await DrawElement($"Time: {estimated} / {spent}",
                    NextElement());
            }

            if (node.Task.Deadlines is { } deadlines)
            {
                var hd = deadlines.HardDeadline?.ToString() ?? "?";
                var sd = deadlines.SoftDeadline.ToString() ?? "?";
                await DrawElement($"Deadlines: {sd} : {hd}", NextElement());
            }

            if (node.Task.Iterations is { } iterations)
            {
                var perIteration = iterations.TimePerIteration?.ToShortString() ?? "?";
                await DrawElement(
                    $"Iterations: {iterations.Executed} / {iterations.Required} x{perIteration}",
                    NextElement());
            }

            if (node.Task.Metrics is { } metrics)
            {
                await DrawElement($"Metrics: C={metrics.Complexity}, I={metrics.Importance}",
                    NextElement());
            }

            if (node.Task.References.Count > 0)
            {
                await DrawElement($"References: {node.Task.References.Count}", NextElement());
            }
        }

        private async Task DrawElement(string text, RenderElement element)
        {
            text = _nodeTextFormatter.ClampText(text, element.MaxLetters);
            var desc = _nodeTextFormatter.ClampText(text, element.MaxLetters);
            await _context.FillTextAsync(desc, element.Position.X, element.Position.Y);
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
            var label = typeLabels.Length switch
            {
                1 => typeLabels[0],
                _ => $"{typeLabels[0]} +{typeLabels.Length - 1}"
            };
            label = _nodeTextFormatter.ClampText(label, edge.Label.MaxLetters);
            await _context.FillTextAsync(label, edge.Label.Position.X, edge.Label.Position.Y);
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
