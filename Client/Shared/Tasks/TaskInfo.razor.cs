using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class TaskInfo
    {
        [Parameter] public Todo Task { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnParametersSet()
        {
            if (Task == null)
            {
                throw new ArgumentException(nameof(Task));
            }
        }
    }
}
