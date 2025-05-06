using System.Collections.Generic;
using CarCollectionApp.Models;

namespace CarCollectionApp.Services
{
    public interface IBrandService
    {
        List<Brand> GetAllBrands();
        Brand? GetBrandById(int id);
        void AddBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(int id);
    }
}
