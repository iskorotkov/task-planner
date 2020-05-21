using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Canvas;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class TaskGraph
    {
        [Inject] public CanvasPainter Painter { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }
        [Inject] public ReferencesAnalyzer Analyzer { get; set; }

        private BECanvasComponent _canvas;
        private PlacementConfig _placementConfig = new PlacementConfig
        {
            ReferenceTypes = ReferenceType.Dependant
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Painter.Init(_canvas);
            var tasks = await TaskManager.GetAll();
            var graph = await Analyzer.Analyze(tasks, placementConfig: _placementConfig);
            await Painter.DrawGraph(graph);
        }
    }
}
