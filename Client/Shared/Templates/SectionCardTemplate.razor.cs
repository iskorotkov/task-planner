using Microsoft.AspNetCore.Components;

namespace TaskPlanner.Client.Shared.Templates
{
    public partial class SectionCardTemplate<TValue>
    {
        [Parameter] public TValue Value { get; set; }
        [Parameter] public RenderFragment EditFields { get; set; }
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public EventCallback RemoveSection { get; set; }
    }
}
