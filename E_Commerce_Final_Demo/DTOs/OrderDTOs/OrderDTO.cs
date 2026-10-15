using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Final_Demo.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        // Pending ,Completed, Cancelled
        public string OrderStatus { get; set; }
        public decimal TotalAmountOrder { get; set; }
        public int UserId { get; set; }
    }
}
