using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Client.Services.Utilities;

namespace TaskPlanner.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("TaskPlanner.ServerAPI",
                    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("TaskPlanner.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddTransient<IRandomStringGenerator, RandomStringGenerator>();

            await builder.Build().RunAsync();
        }
    }
}
