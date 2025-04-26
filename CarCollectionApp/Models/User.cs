using System.ComponentModel.DataAnnotations;

namespace CarCollectionApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime JoinDate { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
