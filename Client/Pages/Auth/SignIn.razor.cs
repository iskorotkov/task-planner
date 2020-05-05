using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        public async Task Submit(EditContext context)
        {
            try
            {
                await AuthManager.SignIn(User);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                // TODO:
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
