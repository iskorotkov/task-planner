using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Services.Auth;

namespace TaskPlanner.Client.Pages.Auth
{
    public partial class SignIn
    {
#pragma warning disable 8618
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public IAuthManager AuthManager { get; set; }
#pragma warning restore 8618

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthManager.StartUi().ConfigureAwait(false);
            }
        }
    }
}
