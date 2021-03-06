﻿using System;
using System.Linq;
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
        private Queue<NodePosition> _nodesQueue;
        private int _nextGraphRow = 0;

        public Task<PlacementGraph> Analyze(AbstractGraph abstractGraph, GraphConfig config)
        {
            _abstractGraph = abstractGraph ?? throw new ArgumentNullException(nameof(abstractGraph));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            if (abstractGraph.Roots.Count == 0)
            {
                return Task.FromResult(new PlacementGraph());
            }

            _nextGraphRow = 0;
            _graph = new PlacementGraph();
            _placedTasks = new HashSet<Todo>();

            foreach (var root in _abstractGraph.Roots)
            {
                AddGraph(root);
            }

            return Task.FromResult(_graph);
        }

        private void AddGraph(AbstractNode root)
        {
            _nodesQueue = new Queue<NodePosition>();
            _nodesQueue.Enqueue(new NodePosition(root, new Position(0, _nextGraphRow)));
            while (_nodesQueue.Count > 0)
            {
                AddNode(_nodesQueue);
            }
        }

        private void AddNode(Queue<NodePosition> nodesQueue)
        {
            var (nextNode, position) = nodesQueue.Dequeue();
            var (column, row) = position;
            CreatePlacementNode(nextNode, row, column);

            var referenceRow = row;
            var referenceColumn = column + 1;
            foreach (var reference in nextNode.References)
            {
                if (_placedTasks.Contains(reference.Node.Task))
                {
                    AddEdgeToExistingNode(reference, column, row);
                }
                else
                {
                    var fromPosition = new Position(column, row);
                    var toPosition = new Position(referenceColumn, referenceRow);
                    var nodePosition = _nodesQueue.FirstOrDefault(x => Equals(x.Node, reference.Node));
                    if (nodePosition == null)
                    {
                        _nodesQueue.Enqueue(new NodePosition(reference.Node, toPosition));
                        referenceRow++;
                    }

                    _graph.Edges.Add(new PlacementEdge(
                        @from: fromPosition,
                        to: nodePosition?.Position ?? toPosition,
                        type: reference.Type
                    ));
                    _nextGraphRow = Math.Max(_nextGraphRow, referenceRow);
                }
            }
        }

        private void CreatePlacementNode(AbstractNode nextNode, int row, int column)
        {
            var placementNode = new PlacementNode(nextNode.Task, new Position(column, row));
            _graph.Nodes.Add(placementNode);
            _placedTasks.Add(nextNode.Task);
        }

        private void AddEdgeToExistingNode(AbstractReference reference, int column, int row)
        {
            var placedNode = _graph.Nodes.Find(x => x.Task == reference.Node.Task);
            _graph.Edges.Add(new PlacementEdge(
                @from: new Position(column, row),
                to: placedNode.Position,
                type: reference.Type
            ));
        }
    }
}
