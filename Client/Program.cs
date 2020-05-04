using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Client.Services.Authentication;
using TaskPlanner.Client.Services.Managers;
using TaskPlanner.Client.Services.Utilities;

namespace TaskPlanner.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<SignOutSessionStateManager>();
            builder.Services.AddHttpClient("Firebase");
            builder.Services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Firebase"));

            builder.Services
                .AddRemoteAuthentication<RemoteAuthenticationState, FirebaseAccount, FirebaseAuthenticationOptions>();
            builder.Services
                .AddScoped<IRemoteAuthenticationService<RemoteAuthenticationState>, FirebaseAuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, FirebaseAuthenticationStateProvider>();

            builder.Services.AddTransient<IRandomStringGenerator, RandomStringGenerator>();
            builder.Services.AddScoped<ITaskManager, TaskManager>();

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
