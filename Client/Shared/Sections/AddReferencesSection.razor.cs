using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared.Data.State;
using Shared.Data.Ui;
using TaskPlanner.Client.Services.References;
using TaskPlanner.Client.Services.Tasks;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class AddReferencesSection
    {
        [Inject] public IReferenceManager ReferenceManager { get; set; }
        [Inject] public ITaskManager TaskManager { get; set; }

        [Parameter] public Todo SelectedTask { get; set; }
        [CascadingParameter] private TaskEditingState TaskEditingState { get; set; }

        private string? OtherTaskId { get; set; }
        private string? TypeLabel { get; set; }

        private List<Todo> _tasks;
        private readonly List<ActionWithLabel<Todo>> _referenceInfo;

        public AddReferencesSection()
        {
            // TODO: #7 Avoid listing all types of references (code duplication)
            _referenceInfo = new List<ActionWithLabel<Todo>>
            {
                new ActionWithLabel<Todo>(AddChild, "Child"),
                new ActionWithLabel<Todo>(AddParent, "Parent"),
                new ActionWithLabel<Todo>(AddDependency, "Dependency"),
                new ActionWithLabel<Todo>(AddDependant, "Dependant"),
                new ActionWithLabel<Todo>(AddAlternative, "Alternative"),
                new ActionWithLabel<Todo>(AddSimilar, "Similar"),
                new ActionWithLabel<Todo>(AddTestFor, "Test for"),
                new ActionWithLabel<Todo>(AddTestedBy, "Tested by")
            };
            TypeLabel = _referenceInfo[0].Label;
        }

        protected override void OnParametersSet()
        {
            if (SelectedTask == null)
            {
                throw new ArgumentNullException(nameof(SelectedTask));
            }

            if (TaskEditingState == null)
            {
                throw new ArgumentNullException(nameof(TaskEditingState));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.GetAll().ConfigureAwait(false);
            if (_tasks.Count > 0)
            {
                OtherTaskId = _tasks[0].Metadata.Id;
            }
        }

        // TODO: #8 Avoid potentially costly task lookup when adding task references
        private async Task<Todo> GetOtherTask()
        {
            if (OtherTaskId == null)
            {
                throw new InvalidOperationException("Can't retrieve object if it's Id wasn't provided.");
            }
            return await TaskManager.Find(OtherTaskId)
                .ConfigureAwait(false)
                ?? throw new ArgumentException("Task with provided Id doesn't exist.");
        }

        private async Task Submit()
        {
            if (OtherTaskId == null || string.IsNullOrWhiteSpace(TypeLabel))
            {
                Console.WriteLine("Task or reference type isn't selected");
                return;
            }

            // TODO: #9 Do not use label as an identifier for type of task reference
            var referenceType = _referenceInfo.Find(x => x.Label == TypeLabel);
            var createReferenceFn = referenceType.Task;

            var task = await GetOtherTask().ConfigureAwait(false);
            if (task == null)
            {
                throw new KeyNotFoundException("Task with provided id wasn't found.");
            }

            TaskEditingState.ModifiedTasks.Add(task);
            await createReferenceFn(task).ConfigureAwait(false);
        }

        private async Task AddChild(Todo task)
        {
            await ReferenceManager.AddChild(task, SelectedTask).ConfigureAwait(false);
        }

        private async Task AddParent(Todo task)
        {
            await ReferenceManager.AddChild(SelectedTask, task).ConfigureAwait(false);
        }

        private async Task AddDependency(Todo task)
        {
            await ReferenceManager.AddDependency(task, SelectedTask).ConfigureAwait(false);
        }

        private async Task AddDependant(Todo task)
        {
            await ReferenceManager.AddDependency(SelectedTask, task).ConfigureAwait(false);
        }

        private async Task AddAlternative(Todo task)
        {
            await ReferenceManager.AddAlternatives(new List<Todo> { SelectedTask, task })
                .ConfigureAwait(false);
        }

        private async Task AddSimilar(Todo task)
        {
            await ReferenceManager.AddSimilar(new List<Todo> { SelectedTask, task })
                .ConfigureAwait(false);
        }

        private async Task AddTestFor(Todo task)
        {
            await ReferenceManager.AddTest(SelectedTask, task).ConfigureAwait(false);
        }

        private async Task AddTestedBy(Todo task)
        {
            await ReferenceManager.AddTest(task, SelectedTask).ConfigureAwait(false);
        }
    }
}
