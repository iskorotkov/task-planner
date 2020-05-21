using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Shared.Graph
{
    public partial class ReferenceTypeSelector
    {
        [Parameter] public Func<ReferenceType, bool>? ShowCheckbox { get; set; }
        [Parameter] public EventCallback<ReferenceType> OnToggle { get; set; }

        public ReferenceType BitMask { get; set; }

        private async Task Toggle(ReferenceType type, bool status)
        {
            if (status)
            {
                BitMask |= type;
            }
            else
            {
                BitMask &= ~type;
            }
            StateHasChanged();
            await OnToggle.InvokeAsync(type);
        }

        private IEnumerable<ReferenceType> GetTypes()
        {
            var types = Enum.GetValues(typeof(ReferenceType))
                .Cast<ReferenceType>()
                .Skip(1);
            if (ShowCheckbox != null)
            {
                return types.Where(ShowCheckbox);
            }
            return types;
        }
    }
}
