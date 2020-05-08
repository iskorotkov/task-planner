using Microsoft.AspNetCore.Components;

namespace TaskPlanner.Client.Shared.Templates
{
    public partial class CardTemplate
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public RenderFragment? Header { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public RenderFragment? Body { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public RenderFragment? Footer { get; set; }
    }
}
