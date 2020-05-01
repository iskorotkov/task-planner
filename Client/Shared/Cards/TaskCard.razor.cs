using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Cards
{
    public partial class TaskCard
    {
        [Parameter] public Todo? Todo { get; set; }

        [Parameter] public EventCallback<Todo> TaskSelected { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; }

        private readonly string _id = "";

        public TaskCard()
        {
            var guid = Guid.NewGuid();
            var chars = guid
                .ToString()
                .Where(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
            _id = new string(chars.ToArray());
        }

        protected override void OnParametersSet()
        {
            if (Todo == null)
            {
                throw new ArgumentException(nameof(Todo));
            }
        }

        private async Task ShowTaskInfo(MouseEventArgs obj)
        {
            if (TaskSelected.HasDelegate)
            {
                await TaskSelected.InvokeAsync(Todo!);
            }
        }
    }
}
