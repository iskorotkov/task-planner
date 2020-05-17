using System.Collections.Generic;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using Xunit;

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
        }
    }
}
