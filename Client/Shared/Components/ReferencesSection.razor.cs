using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.Tasks;

namespace TaskPlanner.Client.Shared.Components
{
    public partial class ReferencesSection
    {
        [Parameter] public Todo Task { get; set; }

        [Inject] public ITaskManager TaskManager { get; set; }

        private List<ReferencedTasks>? ReferencedTasks { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Task == null)
            {
                throw new ArgumentNullException(nameof(Task));
            }

            // TODO: #10 Move fetching referenced tasks into separate service
            var referenceTypes = Task.References
                .Select(r => r.Type)
                .Distinct()
                .ToList();

            var referencedTasks = new List<ReferencedTasks>();
            foreach (var type in referenceTypes)
            {
                var ids = Task.References
                    .Where(reference => reference.Type == type)
                    .Select(reference => reference.TargetId);

                var tasks = new List<Todo>();
                foreach (var id in ids)
                {
                    var task = await TaskManager.Find(id).ConfigureAwait(false)
                              ?? throw new ArgumentException("Task with provided Id wasn't found");
                    tasks.Add(task);
                }

                referencedTasks.Add(new ReferencedTasks(type, tasks));
            }

            ReferencedTasks = referencedTasks;
        }
    }
}
