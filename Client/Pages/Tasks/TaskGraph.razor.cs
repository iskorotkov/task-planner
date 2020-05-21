using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Canvas;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.Client.Shared.Graph;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class TaskGraph
    {
        [Inject] public CanvasPainter Painter { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }
        [Inject] public ReferencesAnalyzer Analyzer { get; set; }

        private BECanvasComponent _canvas;
        private ReferenceTypeSelector _analyzeTypesSelector;
        private ReferenceTypeSelector _showingTypesSelector;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RenderGraph();
            }
        }

        private async Task RenderGraph()
        {
            var painterConfig = new PainterConfig
            {
                Types = _showingTypesSelector.BitMask
            };
            await Painter.Init(_canvas, painterConfig);

            var tasks = await TaskManager.GetAll();
            var placementConfig = new PlacementConfig
            {
                ReferenceTypes = _analyzeTypesSelector.BitMask
            };
            var graph = await Analyzer.Analyze(tasks, placementConfig: placementConfig);

            await Painter.DrawGraph(graph);
        }
    }
}
