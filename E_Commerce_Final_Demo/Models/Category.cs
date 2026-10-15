using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required , StringLength(100,MinimumLength =2)]
        public string CategoryName { get; set; }
        [MaxLength(200)]
        public string? CategoryDescription { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
