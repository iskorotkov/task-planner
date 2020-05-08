using Microsoft.AspNetCore.Components;

namespace TaskPlanner.Client.Shared.Tasks
{
    public partial class AddSection
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback OnClick { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public string? Text { get; set; }
    }
}
