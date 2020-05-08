using System;

namespace TaskPlanner.Shared.Data.Sections
{
    public class Section
    {
        public string TextOnCreateButton { get; }
        public Action OnAdded { get; }
        public Func<bool> CanBeAdded { get; }

        public Section(string textOnCreateButton, Action onAdded, Func<bool> canBeAdded)
        {
            TextOnCreateButton = textOnCreateButton ?? throw new ArgumentNullException(nameof(textOnCreateButton));
            OnAdded = onAdded ?? throw new ArgumentNullException(nameof(onAdded));
            CanBeAdded = canBeAdded ?? throw new ArgumentNullException(nameof(canBeAdded));
        }
    }
}
