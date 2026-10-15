using E_Commerce_Final_Demo.Data;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Reposatories.Implmentation
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    
        public async Task<ICollection<Product>> GetAllProductsWithPagignationsortedByPriceWithOptionaCategorylId(int? Categoryid, int pagenumber, int pagesize)
        {
            //var items =  await _context.Products.AsQueryable()

            //    .Where(p => Categoryid.HasValue)
            //    .OrderByDescending(p => p.ProductPrice)
            //    .Skip((pagenumber - 1) * pagesize)
            //    .Take(pagesize)
            //    .ToListAsync();

            //return items;


            var items = await _context.Products.Where(p => !Categoryid.HasValue || p.CategoryId == Categoryid.Value)
                .OrderByDescending(p => p.ProductPrice)
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return items;

        }

        public async Task<ICollection<Product>> GetMostSecondExpensiveInACategory(int Categoryid)
        {

            var items = await _context.Products
                .Where(p => p.CategoryId == Categoryid)
                .OrderByDescending(p => p.ProductPrice)
                .Skip(1)
                .Take(1).ToListAsync();

            return items;
        }

        public async Task<ICollection<Product>> SearchByName(string name)
        {
            var items = await _context.Products
                .Where(p => p.ProductName.Contains(name))
                .ToListAsync();
            return items;
        }
    }
}
