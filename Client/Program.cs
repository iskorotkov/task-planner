using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskPlanner.Client.Services.Auth;
using TaskPlanner.Client.Services.Canvas;
using TaskPlanner.Client.Services.Storage;
using TaskPlanner.Shared.Services.References;
using TaskPlanner.Shared.Services.Tasks;
using TaskPlanner.TaskGraph.Analyzers;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Layout;

namespace TaskPlanner.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<ITaskManager, TaskManager>();
            builder.Services.AddScoped<IReferenceManager, ReferenceManager>();

            builder.Services.AddGraphAnalyzers();
            builder.Services.AddLayoutBuilders();
            builder.Services.AddTransient<CanvasPainter>();

            builder.Services.AddFirebaseAuthentication();
            builder.Services.AddFirestore();

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
