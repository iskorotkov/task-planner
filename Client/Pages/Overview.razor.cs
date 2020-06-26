using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.Shared.Services.Tasks;

namespace TaskPlanner.Client.Pages
{
    [Authorize]
    public partial class Overview
    {

    }
}
