using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;
using Xunit;

namespace TaskPlanner.TaskGraph.Tests.Analyzers
{
    public class ReferencesAnalyzerTests
    {
        private readonly ReferencesAnalyzer _analyzer = new ReferencesAnalyzer();

        [Fact]
        private void OneTaskWithSingleDependency()
        {
            var task1 = new Todo();
            var task2 = new Todo();
            task1.Metadata.Id = "task1";
            task2.Metadata.Id = "task2";

            task2.References.Add(new Reference(task1.Metadata.Id, ReferenceType.Dependency));
            task1.References.Add(new Reference(task2.Metadata.Id, ReferenceType.Dependant));
            var input = new List<Todo> { task1, task2 };

            var abstractGraph = _analyzer.BuildAbstractGraph(input, new GraphConfig())
                .GetAwaiter().GetResult();

            var placementGraph = _analyzer.BuildPlacementGraph(abstractGraph, new GraphConfig())
                .GetAwaiter().GetResult();
            Assert.Equal(new List<PlacementNode>
            {
                new PlacementNode(task2, new Position(0, 0)),
                new PlacementNode(task1, new Position(1, 0))
            }, placementGraph.Nodes);
            Assert.Equal(new List<PlacementEdge>
            {
                new PlacementEdge(new Position(0, 0), new Position(1, 0), ReferenceType.Dependency),
                new PlacementEdge(new Position(1, 0), new Position(0, 0), ReferenceType.Dependant)
            }, placementGraph.Edges);

            var renderGraph = _analyzer.BuildRenderGraph(placementGraph, new GraphConfig
            {
                HorizontalInterval = 20,
                VerticalInterval = 30,
                LeftOffset = 40,
                TopOffset = 50,
                NodeHeight = 200,
                NodeWidth = 160
            }).GetAwaiter().GetResult();
            Assert.Equal(new List<RenderNode>
            {
                new RenderNode(task2, new Position(40, 50), new Dimensions(160, 200)),
                new RenderNode(task1, new Position(220, 50), new Dimensions(160, 200))
            }, renderGraph.Nodes);
            Assert.Equal(new List<RenderEdge>
            {
                new RenderEdge(new Position(200, 150), new Position(220, 150), ReferenceType.Dependency),
                new RenderEdge(new Position(220, 150), new Position(200, 150), ReferenceType.Dependant)
            }, renderGraph.Edges);
        }
    }
}
