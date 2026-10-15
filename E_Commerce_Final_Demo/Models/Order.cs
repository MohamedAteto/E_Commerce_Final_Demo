using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Final_Demo.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        // Pending ,Completed, Cancelled
        public string OrderStatus { get; set; } 
        [Required]
        public decimal TotalAmountOrder { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
