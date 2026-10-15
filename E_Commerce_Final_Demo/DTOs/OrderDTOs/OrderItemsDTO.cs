namespace E_Commerce_Final_Demo.DTOs.OrderDTOs
{
    public class OrderItemsDTO
    {
        public int OrderItemID { get; set; }
        public int OrderItemQuantity { get; set; }
        public decimal OrderItemPrice { get; set; }
        public decimal TotalAmountOrderItem => OrderItemQuantity * OrderItemPrice;
    }
}
