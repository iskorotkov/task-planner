using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskPlanner.Shared.Data.Ui;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class SectionsManagementSection
    {
        [Parameter] public List<ActionButton> Sections { get; set; }
    }
}
