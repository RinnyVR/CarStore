using Xunit;
using CarCollectionApp.Services;
using CarCollectionApp.Models;

namespace CarCollectionApp.Tests
{
    public class BrandServiceTests
    {
        [Fact]
        public void FormatBrandDisplayName_ReturnsFormattedString()
        {            
            var service = new BrandService(null); 
            var brand = new Brand { Name = "BMW", Country = "Germany" };

            var result = service.FormatBrandDisplayName(brand);

            Assert.Equal("BMW (Germany)", result);
        }

        [Fact]
        public void FormatBrandDisplayName_ReturnsUnknown_ForNullBrand()
        {
            var service = new BrandService(null);

            var result = service.FormatBrandDisplayName(null);

            Assert.Equal("Unknown", result);
        }
    }
}
