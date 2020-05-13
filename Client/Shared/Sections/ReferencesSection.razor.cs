using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Client.Services.References;
using TaskPlanner.Shared.Data.References;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Shared.Sections
{
    public partial class ReferencesSection
    {
        [Parameter] public Todo Task { get; set; }

        [Inject] public IReferenceManager ReferenceManager { get; set; }

        private List<ReferenceType> _referenceTypes;

        protected override void OnParametersSet()
        {
            if (Task == null)
            {
                throw new ArgumentNullException(nameof(Task));
            }

            _referenceTypes = Task.References
                .Select(r => r.Type)
                .Distinct()
                .ToList();
        }

        private IEnumerable<Reference> GetReferencesOfType(ReferenceType t)
        {
            return Task.References.Where(r => r.Type == t);
        }
    }
}
