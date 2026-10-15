using E_Commerce_Final_Demo.Data;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Reposatories.Implmentation
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<User> GetUserByUserNameAsync(string Name)
        {
            var item = await _context.Users.FirstOrDefaultAsync(u => u.UserFullName == Name);
            return item;
        }
    }
}
