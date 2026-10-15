using E_Commerce_Final_Demo.DTOs.OrderDTOs;
using E_Commerce_Final_Demo.Models;

namespace E_Commerce_Final_Demo.Reposatories.Interface
{
    public interface IOrdersRepo : IGenericRepo<Order>
    {
        Task<Order> UpdateOrderStatus(int orderId, string newStatus);
        Task<decimal> GetTotlaRevenue();
        Task<OrderDetialsDTO> GetOrderByIdWithIsItemsAndTotlaAmount(int orderId);
    }
}
