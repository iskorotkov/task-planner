using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Coordinates;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Abstract;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Placement;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class PlacementAnalyzer
    {
        private AbstractGraph _abstractGraph;
        private PlacementGraph _graph;
        private GraphConfig _config;
        private HashSet<Todo> _placedTasks;

        public Task<PlacementGraph> Analyze(AbstractGraph abstractGraph, GraphConfig config)
        {
            _abstractGraph = abstractGraph ?? throw new ArgumentNullException(nameof(abstractGraph));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            if (abstractGraph.Roots.Count == 0)
            {
                return Task.FromResult(new PlacementGraph());
            }

            _graph = new PlacementGraph();
            _placedTasks = new HashSet<Todo>();
            var subgraphRow = 0;
            foreach (var root in _abstractGraph.Roots)
            {
                PlaceSubgraph(root, subgraphRow);
            }

            return Task.FromResult(_graph);
        }

        private void PlaceSubgraph(AbstractNode root, int subgraphRow)
        {
            var nodesQueue = new Queue<(AbstractNode Node, int Row, int Column)>();
            nodesQueue.Enqueue((root, subgraphRow, 0));
            while (nodesQueue.Count > 0)
            {
                var (nextNode, row, column) = nodesQueue.Dequeue();
                var placementNode = new PlacementNode(nextNode.Task, new Position(column, row));
                _graph.Nodes.Add(placementNode);
                _placedTasks.Add(nextNode.Task);

                var referenceRow = row;
                var referenceColumn = column + 1;
                foreach (var reference in nextNode.References)
                {
                    if (_placedTasks.Contains(reference.Node.Task))
                    {
                        var placedNode = _graph.Nodes.Find(x => x.Task == reference.Node.Task);
                        _graph.Edges.Add(new PlacementEdge(
                            from: new Position(column, row),
                            to: placedNode.Position,
                            type: reference.Type
                        ));
                    }
                    else
                    {
                        _graph.Edges.Add(new PlacementEdge(
                            from: new Position(column, row),
                            to: new Position(referenceColumn, referenceRow),
                            type: reference.Type
                        ));

                        nodesQueue.Enqueue((reference.Node, referenceRow, referenceColumn));
                        referenceRow++;
                    }
                }
            }
        }
    }
}
