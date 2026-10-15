using E_Commerce_Final_Demo.Data;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;

namespace E_Commerce_Final_Demo.Reposatories.Implmentation
{
    public class UnitOfWrok : IUnitOfWork
    {
        public IUserRepo UserRepo { get; }

        public IGenericRepo<Category> CategoryRepo { get; }

        public IProductRepo ProductRepo { get; }

        public IOrdersRepo OrdersRepo { get; }

        private readonly AppDbContext _context;
        public UnitOfWrok(AppDbContext context,IUserRepo userRepo , IGenericRepo<Category> categoryRepo , IProductRepo productRepo,IOrdersRepo ordersRepo)
        {
            UserRepo = userRepo;
            CategoryRepo = categoryRepo;
            ProductRepo = productRepo;
            _context = context;
            OrdersRepo = ordersRepo;
        }


        public Task SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }
    }
}
