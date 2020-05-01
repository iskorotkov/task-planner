namespace TaskPlanner.Client.Services.Utilities
{
    public interface IRandomStringGenerator
    {
        string Next(int length = 16);
    }
}
