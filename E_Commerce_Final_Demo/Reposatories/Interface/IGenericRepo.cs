namespace E_Commerce_Final_Demo.Reposatories.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id,T entity);
        Task DeleteAsync(int id);

    }
}
