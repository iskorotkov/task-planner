using System.Collections.Generic;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.References;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;
using TaskPlanner.TaskGraph.Data.Render;
using TaskPlanner.TaskGraph.Layout;
using Xunit;

namespace TaskPlanner.TaskGraph.Tests.Analyzers
{
    public class GraphAnalyzerTests
    {
        private readonly AbstractAnalyzer _abstractAnalyzer = new AbstractAnalyzer();
        private readonly PlacementAnalyzer _placementAnalyzer = new PlacementAnalyzer();
        private readonly RenderAnalyzer _renderAnalyzer = new RenderAnalyzer(new LayoutBuilder());

        private readonly IReferenceManager _referenceManager = new ReferenceManager();
        private readonly GraphConfig _config = new GraphConfig
        {
            Intervals = new Position(20, 30),
            Offsets = new Position(40, 50),
            Dimensions = new Dimensions(160, 200),
            ReferenceTypes = ReferenceType.All,
            ContentPadding = new Padding(11),
            Elements = new List<NodeElement>
            {
                new NodeElement(new Position(15, 30), 16),
                new NodeElement(new Position(0, 15), 9)
            },
            EdgeLabel = new NodeElement(new Position(23, 24), 10),
            BackwardEdgeLabel = new NodeElement(new Position(11, 12), 20)
        };

        private static Todo CreateTask(string id) => new Todo { Metadata = { Id = id } };

        // task2 (dependant) <-> (dependency) task1
        [Fact]
        private void OneTaskWithSingleDependency()
        {
            var task1 = CreateTask("task1");
            var task2 = CreateTask("task2");
            _referenceManager.AddDependency(task1, task2);

            var abstractGraph = _abstractAnalyzer.Analyze(new[] { task1, task2 }, _config)
                .GetAwaiter().GetResult();
            Assert.Single(abstractGraph.Roots);
            Assert.Equal(task2, abstractGraph.Roots[0].Task);
            Assert.Single(abstractGraph.Roots[0].References);
            Assert.Equal(task1, abstractGraph.Roots[0].References[0].Node.Task);
            Assert.Single(abstractGraph.Roots[0].References[0].Node.References);
            Assert.Equal(abstractGraph.Roots[0], abstractGraph.Roots[0].References[0].Node.References[0].Node);

            var placementGraph = _placementAnalyzer.Analyze(abstractGraph, _config)
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

            var renderGraph = _renderAnalyzer.Analyze(placementGraph, _config)
                .GetAwaiter().GetResult();
            var expectedRenderNodes = new List<RenderNode>
            {
                new RenderNode(
                    task2,
                    new Position(40, 50),
                    new Dimensions(160, 200),
                    new List<RenderElement>
                    {
                        new RenderElement(new Position(66, 91), 16),
                        new RenderElement(new Position(66, 106), 9)
                    }
                ),
                new RenderNode(
                    task1,
                    new Position(220, 50),
                    new Dimensions(160, 200),
                    new List<RenderElement>
                    {
                        new RenderElement(new Position(246, 91), 16),
                        new RenderElement(new Position(246, 106), 9)
                    }
                )
            };
            Assert.Equal(expectedRenderNodes.Count, renderGraph.Nodes.Count);
            RenderNodeEqual(expectedRenderNodes[0], renderGraph.Nodes[0]);
            RenderNodeEqual(expectedRenderNodes[1], renderGraph.Nodes[1]);
            Assert.Equal(new List<RenderEdge>
            {
                new RenderEdge(
                    new Position(200, 150), 
                    new Position(220, 150), 
                    ReferenceType.Dependency, 
                    new RenderElement(new Position(233, 174), 10)
                    ),
                new RenderEdge(
                    new Position(220, 150), 
                    new Position(200, 150), 
                    ReferenceType.Dependant, 
                    new RenderElement(new Position(221, 162), 20)
                    )
            }, renderGraph.Edges);
        }

        private void RenderNodeEqual(RenderNode left, RenderNode right)
        {
            Assert.Equal(left.Task, right.Task);
            Assert.Equal(left.Position, right.Position);
            Assert.Equal(left.Dimensions, right.Dimensions);
            Assert.Equal(left.Elements, right.Elements);
        }
    }
}
