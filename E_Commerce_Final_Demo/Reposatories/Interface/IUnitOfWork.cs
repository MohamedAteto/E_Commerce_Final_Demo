using E_Commerce_Final_Demo.Models;

namespace E_Commerce_Final_Demo.Reposatories.Interface
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IGenericRepo<Category> CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        IOrdersRepo OrdersRepo { get; }

        Task SaveChangesAsync();

    }
}
