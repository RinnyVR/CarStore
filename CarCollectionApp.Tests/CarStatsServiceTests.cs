using CarCollectionApp.Services;
using Xunit;

namespace CarCollectionApp.Tests
{
    public class CarStatsServiceTests
    {
        [Fact]
        public void EvaluatePerformance_ShouldReturnHighPerformance_WhenRatioAboveThreshold()
        {
            var service = new CarStatsService();

            var result = service.EvaluatePerformance(500, 40000); // Ratio = 0.0125

            Assert.Equal("High Performance", result);
        }

        [Fact]
        public void EvaluatePerformance_ShouldReturnStandardPerformance_WhenRatioBelowThreshold()
        {
            var service = new CarStatsService();

            var result = service.EvaluatePerformance(300, 50000); // Ratio = 0.006

            Assert.Equal("Standard Performance", result);
        }

        [Fact]
        public void EvaluatePerformance_ShouldReturnInvalidPrice_WhenPriceIsZero()
        {
            var service = new CarStatsService();

            var result = service.EvaluatePerformance(300, 0);

            Assert.Equal("Invalid price", result);
        }
    }
}
