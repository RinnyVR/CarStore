using System.ComponentModel.DataAnnotations;

namespace CarCollectionApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public int Seats { get; set; }

        public int DoorCount { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
