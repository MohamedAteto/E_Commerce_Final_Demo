using E_Commerce_Final_Demo.Models;

namespace E_Commerce_Final_Demo.Reposatories.Interface
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User> GetUserByUserNameAsync(string Name);   
    }

}
