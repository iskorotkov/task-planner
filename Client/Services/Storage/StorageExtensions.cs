using Microsoft.Extensions.DependencyInjection;

namespace TaskPlanner.Client.Services.Storage
{
    public static class StorageExtensions
    {
        public static void AddFirestore(this IServiceCollection collection)
        {
            collection.AddScoped<ITaskStorage, TaskStorage>();
        }
    }
}
