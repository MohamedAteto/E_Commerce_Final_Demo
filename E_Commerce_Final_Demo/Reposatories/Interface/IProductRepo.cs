using E_Commerce_Final_Demo.Models;

namespace E_Commerce_Final_Demo.Reposatories.Interface
{
    public interface IProductRepo : IGenericRepo<Product>
    {
       Task<ICollection<Product>> GetMostSecondExpensiveInACategory(int Categoryid);
       Task<ICollection<Product>> GetAllProductsWithPagignationsortedByPriceWithOptionaCategorylId(int? Categoryid, int pagenumber, int pagesize);
       Task<ICollection<Product>> SearchByName(string name);
    }
}
