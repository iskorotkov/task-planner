using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Properties;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class ExecutionTimeSection
    {
        [Parameter] public ExecutionTime ExecutionTime { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }
    }
}
