namespace CarCollectionApp.Services
{
    public class CarStatsService : ICarStatsService
    {
        public string EvaluatePerformance(int horsepower, decimal price)
        {
            if (price <= 0) return "Invalid price";
            double ratio = (double)horsepower / (double)price;
            return ratio > 0.01 ? "High Performance" : "Standard Performance";
        }
    }
}