﻿using System;
using System.Threading.Tasks;

namespace TaskPlanner.Shared.Data.Ui
{
    public class ActionButton
    {
        public string ButtonText { get; }
        public Func<Task> OnClick { get; }
        public Func<bool> IsActive { get; }

        public string ButtonClass { get; }
        public string ButtonType { get; }

        public ActionButton(
            string textOnCreateButton,
            Func<Task> onAdded,
            Func<bool> canBeAdded,
            string buttonClass = "",
            string buttonType = "button")
        {
            ButtonText = textOnCreateButton ?? throw new ArgumentNullException(nameof(textOnCreateButton));
            OnClick = onAdded ?? throw new ArgumentNullException(nameof(onAdded));
            IsActive = canBeAdded ?? throw new ArgumentNullException(nameof(canBeAdded));
            ButtonClass = buttonClass ?? throw new ArgumentNullException(nameof(buttonClass));
            ButtonType = buttonType ?? throw new ArgumentNullException(nameof(buttonType));
        }
    }
}
