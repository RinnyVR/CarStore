using System.ComponentModel.DataAnnotations;

namespace CarCollectionApp.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
