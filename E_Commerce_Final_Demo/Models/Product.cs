using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Final_Demo.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required , StringLength(100,MinimumLength =2)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string? ProductDescription { get; set; }
        [Required,Range(0.01,1000000)]
        public decimal ProductPrice { get; set; }
        [Required, Range(0, 1000000)]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
