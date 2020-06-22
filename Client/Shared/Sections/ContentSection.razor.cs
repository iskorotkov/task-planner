using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Sections;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class ContentSection
    {
        [Parameter] public Content Content { get; set; }
        [Parameter] public string Title { get; set; }
    }
}
