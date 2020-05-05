using TaskPlanner.Client.Services.Auth;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Auth;

namespace TaskPlanner.Client.Pages.Auth
{
    public partial class Profile
    {
#pragma warning disable 8618
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public IAuthManager AuthManager { get; set; }
#pragma warning restore 8618

        private FirebaseUser? _user;

        protected override async Task OnInitializedAsync()
        {
            _user = await AuthManager.GetUser().ConfigureAwait(false);
            StateHasChanged();
        }
    }
}
