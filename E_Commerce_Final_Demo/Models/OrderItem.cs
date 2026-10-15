using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Final_Demo.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        [Required , Range(1,1000)]
        public int OrderItemQuantity { get; set; }
        [Required , Range(0.01,1000000)]
        public decimal OrderItemPrice { get; set; }


        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
