using System.Collections.Generic;
using System.Linq;
using CarCollectionApp.Models;

namespace CarCollectionApp.Services
{
    public class BrandService : IBrandService
    {
        private readonly CarCollectionContext _context;

        public BrandService(CarCollectionContext context)
        {
            _context = context;
        }

        public List<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public Brand? GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(b => b.Id == id);
        }

        public void AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            var brand = GetBrandById(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }
        }
    }
}
