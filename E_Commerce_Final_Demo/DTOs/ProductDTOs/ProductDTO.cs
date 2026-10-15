using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Final_Demo.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
