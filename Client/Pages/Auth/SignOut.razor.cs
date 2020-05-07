using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskPlanner.Client.Services.Auth;

namespace TaskPlanner.Client.Pages.Auth
{
    public partial class SignOut
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public NavigationManager NavigationManager { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public IAuthManager AuthManager { get; set; }
        [CascadingParameter] public Task<AuthenticationState> State { get; set; }
#pragma warning restore 8618

        protected override async Task OnInitializedAsync()
        {
            if (State == null)
            {
                throw new ArgumentException("Cascading authentication state wasn't provided.", nameof(State));
            }

            var state = await State.ConfigureAwait(false);
            if (state.User.Identity.IsAuthenticated)
            {
                await AuthManager.SignOut().ConfigureAwait(false);
            }
            NavigationManager.NavigateTo("/", true);
        }
    }
}
