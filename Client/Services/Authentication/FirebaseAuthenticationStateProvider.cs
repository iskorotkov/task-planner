using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace TaskPlanner.Client.Services.Authentication
{
    public class FirebaseAuthenticationStateProvider: AuthenticationStateProvider
    {
        private AuthenticationState Anonymous { get; }
        private AuthenticationState CurrentUser { get; set; }
        
        public FirebaseAuthenticationStateProvider()
        {
            Anonymous = new AuthenticationState(new ClaimsPrincipal());
            CurrentUser = Anonymous;
        }
        
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(CurrentUser);
        }
    }
}
