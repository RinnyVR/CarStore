using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarCollectionApp.Models
{
    public class CarCollectionContext : IdentityDbContext<IdentityUser>
    {
        public CarCollectionContext(DbContextOptions<CarCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Dealer)
                .WithMany(d => d.Cars)
                .HasForeignKey(c => c.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Brands
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "BMW", Country = "Germany" },
                new Brand { Id = 2, Name = "Audi", Country = "Germany" },
                new Brand { Id = 3, Name = "Chevrolet", Country = "USA" },
                new Brand { Id = 4, Name = "Ferrari", Country = "Italy" },
                new Brand { Id = 5, Name = "Lamborghini", Country = "Italy" },
                new Brand { Id = 6, Name = "McLaren", Country = "UK" },
                new Brand { Id = 7, Name = "Mercedes-Benz", Country = "Germany" },
                new Brand { Id = 8, Name = "Ford", Country = "USA" },
                new Brand { Id = 9, Name = "Porsche", Country = "Germany" },
                new Brand { Id = 10, Name = "Tesla", Country = "USA" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Type = "Coupe", DoorCount = 2, Seats = 2 },
                new Category { Id = 2, Type = "Sedan", DoorCount = 4, Seats = 5 },
                new Category { Id = 3, Type = "Supercar", DoorCount = 2, Seats = 2 }
            );

            // Seed Dealer
            modelBuilder.Entity<Dealer>().HasData(
                new Dealer
                {
                    Id = 1,
                    Name = "California Dream Cars",
                    City = "Los Angeles",
                    Country = "USA",
                    Email = "contact@calidreamcars.com",
                    Phone = "323-555-9876"
                }
            );

            // Seed Cars
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Model = "BMW M4",
                    Engine = "3.0L Twin-Turbo I6",
                    Horsepower = 503,
                    Price = 85000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "bmw-m4.jpg",
                    BrandId = 1,
                    CategoryId = 1,
                    DealerId = 1
                },
                new Car
                {
                    Id = 2,
                    Model = "Audi R8",
                    Engine = "5.2L V10",
                    Horsepower = 562,
                    Price = 145000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2022,
                    ImagePath = "audi-r8.jpg",
                    BrandId = 2,
                    CategoryId = 3,
                    DealerId = 1
                },
                new Car
                {
                    Id = 3,
                    Model = "Corvette C8",
                    Engine = "6.2L V8",
                    Horsepower = 495,
                    Price = 67000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2022,
                    ImagePath = "corvette-c8.jpg",
                    BrandId = 3,
                    CategoryId = 1,
                    DealerId = 1
                },
                new Car
                {
                    Id = 4,
                    Model = "Ferrari F8",
                    Engine = "3.9L Twin-Turbo V8",
                    Horsepower = 710,
                    Price = 276000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "ferrari-f8.jpg",
                    BrandId = 4,
                    CategoryId = 3,
                    DealerId = 1
                },
                new Car
                {
                    Id = 5,
                    Model = "Lamborghini Huracan",
                    Engine = "5.2L V10",
                    Horsepower = 631,
                    Price = 261000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2022,
                    ImagePath = "lamborghini-huracan.jpg",
                    BrandId = 5,
                    CategoryId = 3,
                    DealerId = 1
                },
                new Car
                {
                    Id = 6,
                    Model = "McLaren 720S",
                    Engine = "4.0L Twin-Turbo V8",
                    Horsepower = 710,
                    Price = 299000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "mclaren-720s.jpg",
                    BrandId = 6,
                    CategoryId = 3,
                    DealerId = 1
                },
                new Car
                {
                    Id = 7,
                    Model = "Mercedes-AMG GT",
                    Engine = "4.0L V8 Biturbo",
                    Horsepower = 523,
                    Price = 118000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "mercedes-amg-gt.jpg",
                    BrandId = 7,
                    CategoryId = 1,
                    DealerId = 1
                },
                new Car
                {
                    Id = 8,
                    Model = "Ford Mustang GT",
                    Engine = "5.0L V8",
                    Horsepower = 450,
                    Price = 55000,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    Year = 2022,
                    ImagePath = "mustang-gt.jpg",
                    BrandId = 8,
                    CategoryId = 1,
                    DealerId = 1
                },
                new Car
                {
                    Id = 9,
                    Model = "Porsche 911",
                    Engine = "3.0L Twin-Turbo Flat-6",
                    Horsepower = 443,
                    Price = 110000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "porsche-911.jpg",
                    BrandId = 9,
                    CategoryId = 1,
                    DealerId = 1
                },
                new Car
                {
                    Id = 10,
                    Model = "Tesla Model S",
                    Engine = "Dual Motor Electric",
                    Horsepower = 1020,
                    Price = 104000,
                    FuelType = "Electric",
                    Transmission = "Automatic",
                    Year = 2023,
                    ImagePath = "tesla-model-s.jpg",
                    BrandId = 10,
                    CategoryId = 2,
                    DealerId = 1
                }
            );
        }
    }
}
