using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Canvas;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskGraph
    {
        [Inject] private CanvasPainter Painter { get; set; }

        private BECanvasComponent _canvas;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Painter.Init(_canvas);
            await Painter.DrawNode(new Point(10, 10), new Dimensions(30, 40));
            await Painter.DrawNode(new Point(100, 10), new Dimensions(30, 40));
            await Painter.DrawEdge(new Point(40, 30), new Point(100, 30));
        }
    }
}
