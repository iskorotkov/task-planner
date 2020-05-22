using Microsoft.Extensions.DependencyInjection;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public static class TaskGraphExtensions
    {
        public static void AddGraphAnalyzers(this IServiceCollection collection)
        {
            collection.AddTransient<AbstractAnalyzer>();
            collection.AddTransient<PlacementAnalyzer>();
            collection.AddTransient<RenderAnalyzer>();
            collection.AddTransient<GraphAnalyzer>();
        }
    }
}
