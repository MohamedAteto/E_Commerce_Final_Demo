using AutoMapper;
using E_Commerce_Final_Demo.DTOs.AuthDTOs;
using E_Commerce_Final_Demo.DTOs.CategoryDTOs;
using E_Commerce_Final_Demo.DTOs.OrderDTOs;
using E_Commerce_Final_Demo.DTOs.ProductDTOs;
using E_Commerce_Final_Demo.Models;

namespace E_Commerce_Final_Demo.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Add your mapping configurations here

            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category,CreateCategoryDTO>().ReverseMap();


            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();


            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<Order,CreateOrderDTO>().ReverseMap();
            CreateMap<Order,UpdateOrderStatusDTO>().ReverseMap();

        }
    }
}
