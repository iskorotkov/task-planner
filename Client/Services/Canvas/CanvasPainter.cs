using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.Client.Services.Canvas
{
    public class CanvasPainter
    {
        private BECanvasComponent? _canvas;
        private Canvas2DContext? _context;

        public async Task Init(BECanvasComponent canvas)
        {
            if (canvas != _canvas)
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

            foreach (var edge in graph.Edges)
            {
                await DrawEdge(edge);
            }
        }

        public async Task DrawNode(RenderNode node)
        {
            EnsureInitialized();
            await _context.SetStrokeStyleAsync("green");
            await _context.SetFillStyleAsync("blue");
            await _context.FillRectAsync(node.Position.X, node.Position.Y, node.Dimensions.Width, node.Dimensions.Height);
        }

        public async Task DrawEdge(RenderEdge edge)
        {
            EnsureInitialized();
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
