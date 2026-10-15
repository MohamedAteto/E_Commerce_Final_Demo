using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Final_Demo.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
