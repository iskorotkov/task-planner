using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Shared.Data.References;

namespace TaskPlanner.Client.Shared.Graph
{
    public partial class ReferenceTypeSelector
    {
        public ReferenceType BitMask { get; private set; }

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
            return Enum.GetValues(typeof(ReferenceType))
                .Cast<ReferenceType>()
                .Skip(1);
        }
    }
}
