using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
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
            foreach (var node in graph.Nodes)
            {
                await DrawNode(node);
            }

            var edges = graph.Edges
                .Where(x => _config.Types.HasFlag(x.Type));
            foreach (var edge in edges)
            {
                await DrawEdge(edge);
            }
        }

        public async Task DrawNode(RenderNode node)
        {
            EnsureInitialized();
            await _context.StrokeRectAsync(node.Position.X, node.Position.Y, node.Dimensions.Width, node.Dimensions.Height);

            var titleElement = node.Elements[0];
            var title = _nodeTextFormatter.ClampText(node.Task.Content.Title ?? "", titleElement.MaxLetters);
            await _context.FillTextAsync(title, titleElement.Position.X, titleElement.Position.Y);

            var descElement = node.Elements[1];
            var desc = _nodeTextFormatter.ClampText(node.Task.Content.Description ?? "", titleElement.MaxLetters);
            await _context.FillTextAsync(desc, descElement.Position.X, descElement.Position.Y);
        }

        public async Task DrawEdge(RenderEdge edge)
        {
            EnsureInitialized();
            await _context.BeginPathAsync();
            await _context.MoveToAsync(edge.From.X, edge.From.Y);
            await _context.LineToAsync(edge.To.X, edge.To.Y);
            await _context.ClosePathAsync();
            await _context.StrokeAsync();

            var labelPos = (edge.From + edge.To) / 2;
            await _context.FillTextAsync(edge.Type.ToString(), labelPos.X, labelPos.Y);
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
