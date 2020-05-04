using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Client.Services.Utilities;
using TaskPlanner.Client.Services.Managers;

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

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
