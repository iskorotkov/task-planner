using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Canvas;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.Client.Shared.Graph;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class TaskGraph
    {
        [Inject] public CanvasPainter Painter { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }
        [Inject] public GraphAnalyzer Analyzer { get; set; }

        private BECanvasComponent _canvas;
        private ReferenceTypeSelector _analyzeTypesSelector;
        private ReferenceTypeSelector _showingTypesSelector;
        private RenderGraph _graph;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AnalyzeGraph();
            }
        }

        private async Task AnalyzeGraph()
        {
            _showingTypesSelector.BitMask = _analyzeTypesSelector.BitMask;
            var tasks = await TaskManager.GetAll();
            var config = new GraphConfig
            {
                ReferenceTypes = _analyzeTypesSelector.BitMask
            };
            _graph = await Analyzer.Analyze(tasks, config);
            await RenderGraph();
        }

        private async Task RenderGraph()
        {
            var painterConfig = new PainterConfig
            {
                Types = _showingTypesSelector.BitMask
            };
            await Painter.Init(_canvas, painterConfig);
            await Painter.DrawGraph(_graph);
        }
    }
}
