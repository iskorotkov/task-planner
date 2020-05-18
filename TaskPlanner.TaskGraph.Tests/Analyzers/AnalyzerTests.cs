using System.Collections.Generic;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using Xunit;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.Shared.Data.Coordinates;

namespace TaskPlanner.TaskGraph.Tests.Analyzers
{
    public class TaskListAnalyzerTests
    {
        private readonly Analyzer _analyzer = new Analyzer();

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
            var placementGraph = _analyzer.BuildPlacementGraph(input);

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

            var renderGraph = _analyzer.BuildRenderGraph(placementGraph, new Config());
        }
    }
}
