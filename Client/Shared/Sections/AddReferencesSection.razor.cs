using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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

        private Todo OtherTask { get; set; }
        private Func<Task> AddReferenceFn { get; set; }

        private List<Todo> _tasks;
        private readonly List<ActionWithLabel> _referenceInfo;

        public AddReferencesSection()
        {
            // TODO: #7 Avoid listing all types of references (code duplication)
            _referenceInfo = new List<ActionWithLabel>
            {
                new ActionWithLabel(AddChild, "Child"),
                new ActionWithLabel(AddParent, "Parent"),
                new ActionWithLabel(AddDependency, "Dependency"),
                new ActionWithLabel(AddDependant, "Dependant"),
                new ActionWithLabel(AddAlternative, "Alternative"),
                new ActionWithLabel(AddSimilar, "Similar"),
                new ActionWithLabel(AddTestFor, "Test for"),
                new ActionWithLabel(AddTestedBy, "Tested by")
            };
        }

        protected override void OnParametersSet()
        {
            if (SelectedTask == null)
            {
                throw new ArgumentNullException(nameof(SelectedTask));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _tasks = await TaskManager.GetAll().ConfigureAwait(false);
        }

        private async Task Submit()
        {
            if (OtherTask == null || AddReferenceFn == null)
            {
                Console.WriteLine("Task or reference type isn't selected");
                return;
            }

            await AddReferenceFn().ConfigureAwait(false);
        }

        private async Task AddChild()
        {
            await ReferenceManager.AddChild(OtherTask, SelectedTask).ConfigureAwait(false);
        }

        private async Task AddParent()
        {
            await ReferenceManager.AddChild(SelectedTask, OtherTask).ConfigureAwait(false);
        }

        private async Task AddDependency()
        {
            await ReferenceManager.AddDependency(OtherTask, SelectedTask).ConfigureAwait(false);
        }

        private async Task AddDependant()
        {
            await ReferenceManager.AddDependency(SelectedTask, OtherTask).ConfigureAwait(false);
        }

        private async Task AddAlternative()
        {
            await ReferenceManager.AddAlternatives(new List<Todo> { SelectedTask, OtherTask })
                .ConfigureAwait(false);
        }

        private async Task AddSimilar()
        {
            await ReferenceManager.AddSimilar(new List<Todo> { SelectedTask, OtherTask })
                .ConfigureAwait(false);
        }

        private async Task AddTestFor()
        {
            await ReferenceManager.AddTest(SelectedTask, OtherTask).ConfigureAwait(false);
        }

        private async Task AddTestedBy()
        {
            await ReferenceManager.AddTest(OtherTask, SelectedTask).ConfigureAwait(false);
        }
    }
}
