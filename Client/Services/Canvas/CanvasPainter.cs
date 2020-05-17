using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using TaskPlanner.Shared.Data.Coordinates;

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
        }

        public async Task DrawNode(Position point, Dimensions dimensions)
        {
            EnsureInitialized();
            await _context.SetStrokeStyleAsync("green");
            await _context.SetFillStyleAsync("blue");
            await _context.FillRectAsync(point.X, point.Y, dimensions.Width, dimensions.Height);
        }

        public async Task DrawEdge(Position from, Position to)
        {
            EnsureInitialized();
            await _context.BeginPathAsync();
            await _context.MoveToAsync(from.X, from.Y);
            await _context.LineToAsync(to.X, to.Y);
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
