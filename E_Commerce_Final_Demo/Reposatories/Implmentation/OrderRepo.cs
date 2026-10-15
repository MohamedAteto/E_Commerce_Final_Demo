using Azure.Core;
using E_Commerce_Final_Demo.Data;
using E_Commerce_Final_Demo.DTOs.OrderDTOs;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Reposatories.Implmentation
{
    public class OrderRepo : GenericRepo<Order>, IOrdersRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderDetialsDTO> GetOrderByIdWithIsItemsAndTotlaAmount(int orderId)
        {
            var item = await _context.Orders
                .Include(o => o.OrderItems)
                .Select(order => new OrderDetialsDTO
                {
                    OrderID = order.OrderID,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalAmountOrder = order.TotalAmountOrder,
                    UserId = order.UserId,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
                    {
                        OrderItemID = oi.OrderItemID,

                        OrderItemQuantity = oi.OrderItemQuantity,
                        OrderItemPrice = oi.OrderItemPrice,

                    })
                    .ToList()

                })
                .FirstOrDefaultAsync(o => o.OrderID == orderId);


           

            return item;
        }

        public async Task<decimal> GetTotlaRevenue()
        {
            var totalRevenue = await _context.Orders.SumAsync(o => o.TotalAmountOrder);
            return totalRevenue;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, string newStatus)
        {
            var item = await _context.Orders.FindAsync(orderId);
            if (item is null)
                throw new Exception($"Order with id {orderId} not found.");

            if(newStatus != "Pending" && newStatus != "Completed" && newStatus != "Cancelled")
                throw new Exception($"Invalid order status: {newStatus}. Valid statuses are: Pending, Completed, Cancelled.");


            item.OrderStatus = newStatus;
            return item;
        }
    }
}
