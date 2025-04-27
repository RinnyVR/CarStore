namespace CarCollectionApp.Services
{
    public interface ICarStatsService
    {
        string EvaluatePerformance(int horsepower, decimal price);
    }
}
