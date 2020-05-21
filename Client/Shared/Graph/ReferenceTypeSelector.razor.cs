using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Shared.Graph
{
    public partial class ReferenceTypeSelector
    {
        [Parameter] public Func<ReferenceType, bool>? ShowCheckbox { get; set; }

        public ReferenceType BitMask { get; set; }

        private void Toggle(ReferenceType type, bool status)
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
