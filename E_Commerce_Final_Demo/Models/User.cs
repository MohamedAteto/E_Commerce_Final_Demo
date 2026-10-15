using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Models
{
    [Index(nameof(UserEmail), IsUnique = true)]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required , StringLength(100,MinimumLength = 3)]
        public string UserFullName { get; set; }
        [Required,EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string UserPasswordHash { get; set; }
        [Required]
        public string UserRole { get; set; } = "Customer";



        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
