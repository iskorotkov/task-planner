using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

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

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
