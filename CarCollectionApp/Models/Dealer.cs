using System.ComponentModel.DataAnnotations;

namespace CarCollectionApp.Models
{
    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
