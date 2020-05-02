﻿using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Shared.Data.Tasks;

namespace TaskPlanner.Client.Pages.Tasks
{
    public partial class UpdateTask
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public ITaskManager TaskManager { get; set; }
#pragma warning restore 8618

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
#pragma warning disable 8618
        [Parameter] public string GuidStr { get; set; }
#pragma warning restore 8618

#pragma warning disable 8618
        private Todo _task;
#pragma warning restore 8618

        private Guid _guid;

#pragma warning disable 8618
        private EditContext _context;
#pragma warning restore 8618

        protected override async Task OnInitializedAsync()
        {
            _guid = Guid.Parse(GuidStr);
            _task = await TaskManager.Find(_guid).ConfigureAwait(false);
            if (_task == null)
            {
                throw new ArgumentException(nameof(_task));
            }

            _context = new EditContext(_task);
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/overview");
        }

        private void Submit()
        {
            NavigationManager.NavigateTo("/overview");
        }

        private async Task Delete()
        {
            await TaskManager.Remove(_task).ConfigureAwait(false);
            NavigationManager.NavigateTo("/overview");
        }
    }
}