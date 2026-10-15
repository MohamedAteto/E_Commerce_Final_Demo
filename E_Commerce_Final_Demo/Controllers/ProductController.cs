using AutoMapper;
using E_Commerce_Final_Demo.DTOs.ProductDTOs;
using E_Commerce_Final_Demo.Mapping;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Final_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork unit)
        {
            this.unit = unit;
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }

        [HttpGet("GetWith pagignation")]
        public async Task<IActionResult> GetWithPagignation(int? id, int pageNumber, int pageSize)
        {
            var products = await unit.ProductRepo.GetAllProductsWithPagignationsortedByPriceWithOptionaCategorylId(id, pageNumber, pageSize);
            if(products == null || !products.Any())
                return NotFound("No products found.");
            
            var productsDto = mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await unit.ProductRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            var productDto = mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpGet("Get Second most expensive")]
        public async Task<IActionResult> GetSecondMostExpensiveProduct(int id)
        {
            var product = await unit.ProductRepo.GetMostSecondExpensiveInACategory(id);

            if (product == null)
                return NotFound("No products found.");

            var productDto = mapper.Map<List<ProductDTO>>(product);
            return Ok(productDto);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null.");

            var category = await unit.CategoryRepo.GetByIdAsync(productDto.CategoryId);
            if (category == null)
                return BadRequest($"Category with ID {productDto.CategoryId} does not exist.");

            var product = mapper.Map<Product>(productDto);

            await unit.ProductRepo.AddAsync(product);
            await unit.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, productDto);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null.");

            var existingProduct = await unit.ProductRepo.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found.");

            var category = await unit.CategoryRepo.GetByIdAsync(productDto.CategoryId);
            if (category == null)
                return BadRequest($"Category with ID {productDto.CategoryId} does not exist.");


            mapper.Map(productDto, existingProduct);
            unit.ProductRepo.UpdateAsync(id,existingProduct);
            await unit.SaveChangesAsync();
            return Ok("Updated succefully");
        }

        [HttpPatch("UpdateStock")]
        public async Task<IActionResult> UpdateProductStock(int id, [FromBody] int newStockQuantity)
        {
            var existingProduct = await unit.ProductRepo.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found.");

            existingProduct.StockQuantity = newStockQuantity;

            unit.ProductRepo.UpdateAsync(id, existingProduct);

            await unit.SaveChangesAsync();
            return Ok("Stock quantity updated successfully.");
        }


        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchProductsByName(string name)
        {
            var products = await unit.ProductRepo.SearchByName(name);
            if (products == null || !products.Any())
                return NotFound("No products found.");


            var productsDto = mapper.Map<List<ProductDTO>>(products);
            return Ok(productsDto);
        }
    }
}
