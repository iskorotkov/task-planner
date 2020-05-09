using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class AddSection
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public EventCallback OnClick { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] public string? Text { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
