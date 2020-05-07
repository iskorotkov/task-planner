using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Client.Services.Auth;
using TaskPlanner.Client.Services.Utilities;
using TaskPlanner.Client.Services.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TaskPlanner.Client.Services.Storage;

namespace TaskPlanner.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient<IRandomStringGenerator, RandomStringGenerator>();
            builder.Services.AddScoped<ITaskManager, TaskManager>();

            // TODO: Extract into extension method
            builder.Services.AddOptions();
            builder.Services.AddAuthenticationCore();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<FirebaseAuthManager>();
            builder.Services.AddScoped<IAuthManager>(provider =>
                provider.GetRequiredService<FirebaseAuthManager>());
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<FirebaseAuthManager>());
            
            builder.Services.AddFirestore();

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
