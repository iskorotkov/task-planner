using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Abstract;
using TaskPlanner.TaskGraph.Data.Config;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class AbstractAnalyzer
    {
        private HashSet<AbstractNode> _nodes;
        private HashSet<AbstractNode> _roots;
        private IEnumerable<Todo> _tasks;
        private GraphConfig _config;

        public Task<AbstractGraph> Analyze(IEnumerable<Todo> tasks, GraphConfig config)
        {
            _tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            var notVisited = tasks.ToHashSet();
            if (!notVisited.Any())
            {
                return Task.FromResult(new AbstractGraph());
            }

            _nodes = new HashSet<AbstractNode>();
            _roots = new HashSet<AbstractNode>();
            
            AnalyzeUnvisited(notVisited);
            RemoveOneNodeGraphs();
            return Task.FromResult(new AbstractGraph(_roots.ToList()));
        }

        private void AnalyzeUnvisited(HashSet<Todo> unvisited)
        {
            while (unvisited.Count > 0)
            {
                var taskToAdd = unvisited.First();
                unvisited.Remove(taskToAdd);
                AnalyzeTask(taskToAdd);
            }
        }

        private void RemoveOneNodeGraphs()
        {
            _roots.RemoveWhere(x => x.References.Count == 0);
        }
        
        private void AnalyzeTask(Todo task)
        {
            var nodeToAdd = GetNodeForTask(task);
            var references = task.References
                .Where(r => _config.ReferenceTypes.HasFlag(r.Type));
            foreach (var reference in references)
            {
                AddReference(nodeToAdd, reference);
            }
        }

        private void AddReference(AbstractNode nodeToAdd, Reference reference)
        {
            var referencedNode = _nodes.FirstOrDefault(x => x.Task.Metadata.Id == reference.TargetId);
            if (referencedNode != null)
            {
                if (_roots.Contains(referencedNode))
                {
                    UpdateRoots(nodeToAdd, referencedNode);
                }
            }
            else
            {
                referencedNode = CreateNodeForReferencedTask(reference);
            }

            nodeToAdd.References.Add(new AbstractReference(referencedNode, reference.Type));
        }

        private void UpdateRoots(AbstractNode nodeToAdd, AbstractNode referencedNode)
        {
            _roots.Remove(referencedNode);
            if (!_roots.Contains(nodeToAdd))
            {
                _roots.Add(nodeToAdd);
            }
        }

        private AbstractNode CreateNodeForReferencedTask(Reference reference)
        {
            var referencedTask = _tasks.FirstOrDefault(x => x.Metadata.Id == reference.TargetId)
                                 ?? throw new ArgumentException(
                                     "One of the provided tasks has unresolved reference to other task. " +
                                     "Probably referenced task was deleted or wasn't created at all.");
            return new AbstractNode(referencedTask);
        }

        private AbstractNode GetNodeForTask(Todo task)
        {
            var nodeToAdd = _nodes.FirstOrDefault(x => x.Task == task);
            if (nodeToAdd == null)
            {
                nodeToAdd = new AbstractNode(task);
                _nodes.Add(nodeToAdd);
                _roots.Add(nodeToAdd);
            }

            return nodeToAdd;
        }
    }
}
