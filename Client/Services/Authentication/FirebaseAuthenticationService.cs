using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace TaskPlanner.Client.Services.Authentication
{
    public class FirebaseAuthenticationService :
        RemoteAuthenticationService<RemoteAuthenticationState, FirebaseAccount, FirebaseAuthenticationOptions>
    {
        public FirebaseAuthenticationService(IJSRuntime jsRuntime,
            IOptions<RemoteAuthenticationOptions<FirebaseAuthenticationOptions>> options,
            NavigationManager navigation,
            AccountClaimsPrincipalFactory<FirebaseAccount> accountClaimsPrincipalFactory) :
            base(jsRuntime, options, navigation, accountClaimsPrincipalFactory)
        {
        }
    }
}
