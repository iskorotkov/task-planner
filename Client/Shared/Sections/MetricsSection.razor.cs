using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Properties;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class MetricsSection
    {
        [Parameter] public Metrics Metrics { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }
    }
}
