using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Properties;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class IterationsSection
    {
        [Parameter] public Iterations Iterations { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }
    }
}
