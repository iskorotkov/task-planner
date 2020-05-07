using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace TaskPlanner.Client.Services.Auth
{
    public static class AuthExtensions
    {
        public static void AddFirebaseAuthentication(this IServiceCollection collection)
        {
            collection.AddOptions();
            collection.AddAuthenticationCore();
            collection.AddAuthorizationCore();

            collection.AddScoped<FirebaseAuthManager>();
            collection.AddScoped<IAuthManager>(provider =>
                provider.GetRequiredService<FirebaseAuthManager>());
            collection.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<FirebaseAuthManager>());
        }
    }
}
