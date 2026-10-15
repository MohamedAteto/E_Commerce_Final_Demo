using AutoMapper;
using E_Commerce_Final_Demo.DTOs.OrderDTOs;
using E_Commerce_Final_Demo.Mapping;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Final_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public OrderController(IUnitOfWork unit)
        {
            this.unit = unit;
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Order data is null.");

            var user = await unit.UserRepo.GetByIdAsync(DTO.UserId);
            if (user == null)
                return NotFound($"User with ID {DTO.UserId} not found.");
            var Entity = mapper.Map<Order>(DTO);

            await unit.OrdersRepo.AddAsync(Entity);
            await unit.SaveChangesAsync();
            return Ok("Order added successfully.");
        }

        [HttpGet("GetOrderByIdWithItems/{orderId}")]
        public async Task<IActionResult> GetOrderByIdWithItems(int orderId)
        {
            var order = await unit.OrdersRepo.GetOrderByIdWithIsItemsAndTotlaAmount(orderId);

            if(order == null)
                return NotFound($"Order with ID {orderId} not found.");


            return Ok(order);
        }

        [HttpPatch("UpdateStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDTO DTO)
        {
            var order = await unit.OrdersRepo.GetByIdAsync(orderId);
            if (order == null)
                return NotFound($"Order with ID {orderId} not found.");

            order.OrderStatus = DTO.OrderStatus;
            await unit.SaveChangesAsync();

            return Ok($"Order status updated to '{DTO}' for Order ID {orderId}.");
        }

        [HttpGet("GetTotalRevenue")]    
        public async Task<IActionResult> GetTotalRevenue()
        {
            var totalRevenue = await unit.OrdersRepo.GetTotlaRevenue();
            return Ok(totalRevenue);
        }
    }
}
