using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Final_Demo.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
