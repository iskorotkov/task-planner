using System.Collections.Generic;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using Xunit;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.Shared.Data.Coordinates;

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
            var placementGraph = _analyzer.BuildPlacementGraph(input)
                .GetAwaiter()
                .GetResult();

            Assert.Equal(2, placementGraph.Nodes.Count);
            Assert.Equal(2, placementGraph.Edges.Count);

            Assert.Equal(task1, placementGraph.Nodes[0].Task);
            Assert.Equal(new Position(0, 0), placementGraph.Nodes[0].Position);

            Assert.Equal(task2, placementGraph.Nodes[1].Task);
            Assert.Equal(new Position(1, 0), placementGraph.Nodes[1].Position);

            Assert.Equal(new Position(0, 0), placementGraph.Edges[0].From);
            Assert.Equal(new Position(1, 0), placementGraph.Edges[0].To);

            Assert.Equal(new Position(1, 0), placementGraph.Edges[1].From);
            Assert.Equal(new Position(0, 0), placementGraph.Edges[1].To);

            var config = new Config
            {
                HorizontalInterval = 20,
                VerticalInterval = 30,
                LeftOffset = 40,
                TopOffset = 50,
                NodeHeight = 200,
                NodeWidth = 160
            };
            var renderGraph = _analyzer.BuildRenderGraph(placementGraph, config)
                .GetAwaiter()
                .GetResult();

            Assert.Equal(2, renderGraph.Nodes.Count);
            Assert.Equal(2, renderGraph.Edges.Count);

            Assert.Equal(task1, renderGraph.Nodes[0].Task);
            Assert.Equal(new Position(40, 50), renderGraph.Nodes[0].Position);
            Assert.Equal(new Dimensions(160, 200), renderGraph.Nodes[0].Dimensions);

            Assert.Equal(task2, renderGraph.Nodes[1].Task);
            Assert.Equal(new Position(220, 50), renderGraph.Nodes[1].Position);
            Assert.Equal(new Dimensions(160, 200), renderGraph.Nodes[1].Dimensions);

            Assert.Equal(new Position(200, 150), renderGraph.Edges[0].From);
            Assert.Equal(new Position(220, 150), renderGraph.Edges[0].To);

            Assert.Equal(new Position(220, 150), renderGraph.Edges[1].From);
            Assert.Equal(new Position(200, 150), renderGraph.Edges[1].To);
        }
    }
}
