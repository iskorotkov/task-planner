using Microsoft.Extensions.DependencyInjection;

namespace TaskPlanner.TaskGraph.Layout
{
    public static class LayoutExtensions
    {
        public static void AddLayoutBuilders(this IServiceCollection collection)
        {
            collection.AddTransient<LayoutBuilder>();
        }
    }
}
