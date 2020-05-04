using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TaskPlanner.Client.Data.Auth;
using TaskPlanner.Client.Services.Auth;

namespace TaskPlanner.Client.Pages.Auth
{
    public partial class SignIn
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
#pragma warning disable 8618
        [Inject] public NavigationManager NavigationManager { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] public IAuthManager AuthManager { get; set; }
#pragma warning restore 8618

        private SignInUser User { get; } = new SignInUser();

        public void GoBack()
        {
            NavigationManager.NavigateTo("/");
        }

        public async Task Submit()
        {
            try
            {
                await AuthManager.SignIn(User);
            }
            catch (Exception e)
            {
                // TODO:
                Console.WriteLine(e);
                throw;
            }

            NavigationManager.NavigateTo("/");
        }
    }
}
