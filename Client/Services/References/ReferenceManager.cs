using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Data.References;
using System;

namespace TaskPlanner.Client.Services.References
{
    public class ReferenceManager : IReferenceManager
    {
        public async Task AddChild(Todo child, Todo parent)
        {
            child.References.Add(new Reference(parent.Metadata.Id!, ReferenceType.Parent));
            parent.References.Add(new Reference(child.Metadata.Id!, ReferenceType.Child));
            // TODO: Save to DB
        }

        public async Task AddDependency(Todo dependency, Todo dependant)
        {
            dependency.References.Add(new Reference(dependant.Metadata.Id!, ReferenceType.Dependant));
            dependant.References.Add(new Reference(dependency.Metadata.Id!, ReferenceType.Dependency));
            // TODO: Save to DB
        }

        public async Task AddAlternatives(List<Todo> alternatives)
        {
            foreach (var alternative in alternatives)
            {
                foreach (var other in alternatives)
                {
                    if (alternative == other)
                    {
                        continue;
                    }

                    alternative.References.Add(new Reference(other.Metadata.Id!, ReferenceType.Alternative));
                    // TODO: Save to DB
                }
            }
        }

        public async Task AddSimilar(List<Todo> similarTasks)
        {
            foreach (var task in similarTasks)
            {
                foreach (var other in similarTasks)
                {
                    if (task == other)
                    {
                        continue;
                    }

                    task.References.Add(new Reference(other.Metadata.Id!, ReferenceType.Similar));
                    // TODO: Save to DB
                }
            }
        }

        public async Task AddTest(Todo test, Todo tested)
        {
            test.References.Add(new Reference(tested.Metadata.Id!, ReferenceType.TestFor));
            tested.References.Add(new Reference(test.Metadata.Id!, ReferenceType.TestedBy));
            // TODO: Save to DB
        }

        public async Task RemoveAlternatives(List<Todo> alternatives)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveChild(Todo child, Todo parent)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDependency(Todo dependency, Todo dependant)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSimilar(List<Todo> similarTasks)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTest(Todo test, Todo tested)
        {
            throw new NotImplementedException();
        }
    }
}
