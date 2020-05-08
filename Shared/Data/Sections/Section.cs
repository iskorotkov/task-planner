using System;

namespace TaskPlanner.Shared.Data.Sections
{
    public class Section
    {
        public string TextOnCreateButton { get; set; }
        public Action OnAdded { get; set; }
        public Func<bool> CanBeAdded { get; set; }
    }
}
