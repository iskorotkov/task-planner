using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Utilities;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Cards
{
    public partial class TaskCard
    {
        [Parameter] public Todo? Todo { get; set; }

        [Parameter] public EventCallback<Todo> TaskSelected { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [Inject] public IRandomStringGenerator Rsg { get; set; }

        private string _id;

        protected override void OnParametersSet()
        {
            if (Todo == null)
            {
                throw new ArgumentException(nameof(Todo));
            }
        }

        protected override void OnInitialized()
        {
            _id = Rsg.Next();
        }
    }
}
