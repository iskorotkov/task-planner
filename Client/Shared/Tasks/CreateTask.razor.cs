using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class CreateTask
    {
        [Inject] public ITaskManager TaskManager { get; set; }

        [Parameter] public EventCallback<Todo> TaskCreated { get; set; }
        // [Parameter] public EventCallback? CreationCanceled { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        private readonly Todo _task;
        private readonly EditContext _context;

        public CreateTask()
        {
            _task = new Todo();
            _context = new EditContext(_task);
        }

        private async Task Submit()
        {
            if (_context.Validate())
            {
                await TaskManager.Add(_task);
                if (TaskCreated.HasDelegate)
                {
                    await TaskCreated.InvokeAsync(_task);
                }
            }
        }
    }
}
