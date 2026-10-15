using AutoMapper;
using E_Commerce_Final_Demo.DTOs.CategoryDTOs;
using E_Commerce_Final_Demo.Mapping;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Final_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public CategoryController(IUnitOfWork unit)
        {
            this.unit = unit;
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var Categories = await unit.CategoryRepo.GetAllAsync();

            if (Categories is null || !Categories.Any())
                return NotFound("No Categories Found");


            var DTOs = mapper.Map<IEnumerable<CategoryDTO>>(Categories);
            return Ok(DTOs);
        }


        [HttpPost("AddCategory")]

        public async Task<IActionResult> Create ([FromBody] CreateCategoryDTO DTO)
        {
            if (DTO is null)
                return BadRequest("Invalid Data");

            var Entity = mapper.Map<Category>(DTO);

            await unit.CategoryRepo.AddAsync(Entity);
            await unit.SaveChangesAsync();
            return Ok("Category Added Successfully");
        }



    }
}
