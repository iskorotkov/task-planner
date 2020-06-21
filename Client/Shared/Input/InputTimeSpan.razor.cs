using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Spans;

namespace TaskPlanner.Client.Shared.Input
{
    public partial class InputTimeSpan
    {
        [Parameter] public TaskTimeSpan Value { get; set; }
    }
}
