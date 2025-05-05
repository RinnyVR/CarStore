using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarCollectionApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        public string Engine { get; set; }

        public int Horsepower { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }

        public string Transmission { get; set; }

        public string FuelType { get; set; }

        public string? ImagePath { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int DealerId { get; set; }
        public Dealer Dealer { get; set; }


        public ICollection<Review>? Reviews { get; set; }
    }
}
