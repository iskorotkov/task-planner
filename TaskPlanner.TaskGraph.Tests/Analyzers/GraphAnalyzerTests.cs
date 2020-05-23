using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.References;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;
using Xunit;

namespace TaskPlanner.TaskGraph.Tests.Analyzers
{
    public class GraphAnalyzerTests
    {
        private readonly AbstractAnalyzer _abstractAnalyzer = new AbstractAnalyzer();
        private readonly PlacementAnalyzer _placementAnalyzer = new PlacementAnalyzer();
        private readonly RenderAnalyzer _renderAnalyzer = new RenderAnalyzer();

        private readonly IReferenceManager _referenceManager = new ReferenceManager();

        private static Todo CreateTask(string id) => new Todo { Metadata = { Id = id } };

        [Fact]
        private void OneTaskWithSingleDependency()
        {
            var task1 = CreateTask("task1");
            var task2 = CreateTask("task2");
            _referenceManager.AddDependency(task1, task2);
            var input = new List<Todo> { task1, task2 };

            var abstractGraph = _abstractAnalyzer.Analyze(input, new GraphConfig())
                .GetAwaiter().GetResult();
            Assert.Single(abstractGraph.Roots);
            Assert.Equal(task2, abstractGraph.Roots[0].Task);
            Assert.Single(abstractGraph.Roots[0].References);
            Assert.Equal(task1, abstractGraph.Roots[0].References[0].Node.Task);
            Assert.Single(abstractGraph.Roots[0].References[0].Node.References);
            Assert.Equal(abstractGraph.Roots[0], abstractGraph.Roots[0].References[0].Node.References[0].Node);

            var placementGraph = _placementAnalyzer.Analyze(abstractGraph, new GraphConfig())
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

            var renderGraph = _renderAnalyzer.Analyze(placementGraph, new GraphConfig
            {
                Intervals = new Position(20, 30),
                Offsets = new Position(40, 50),
                Dimensions = new Dimensions(160, 200),
                ReferenceTypes = ReferenceType.All
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
