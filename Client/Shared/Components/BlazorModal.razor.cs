using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TaskPlanner.Client.Shared.Components
{
    public partial class BlazorModal
    {
        [Parameter] public bool UseFade { get; set; } = true;
        [Parameter] public bool IsShown { get; set; } = false;
        [Parameter] public bool UseBackdrop { get; set; } = true;

        [Parameter] public RenderFragment ModalHeader { get; set; }
        [Parameter] public RenderFragment ModalBody { get; set; }
        [Parameter] public RenderFragment ModalFooter { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        public void Show() => IsShown = true;
        public void Hide() => IsShown = false;
        public void Toggle() => IsShown = !IsShown;

        private string FadeClass => UseFade ? "fade" : null;
        private string ShowClass => IsShown ? "show" : null;
        private string DisplayStyle => IsShown ? "display: block;" : "display: none;";
    }
}
