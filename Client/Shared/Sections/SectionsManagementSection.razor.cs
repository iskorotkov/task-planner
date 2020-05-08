using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Sections;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class SectionsManagementSection
    {
        [Parameter] public List<Section> Sections { get; set; }
    }
}
