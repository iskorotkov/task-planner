using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Shared.Graph
{
    public partial class ReferenceTypeSelector
    {
        private ReferenceType _bitMask { get; set; }

        private void Toggle(ReferenceType type, bool status)
        {
            if (status)
            {
                _bitMask |= type;
            }
            else
            {
                _bitMask &= ~type;
            }
            StateHasChanged();
        }

        private IEnumerable<ReferenceType> GetTypes()
        {
            return Enum.GetValues(typeof(ReferenceType))
                .Cast<ReferenceType>()
                .Skip(1);
        }
    }
}
