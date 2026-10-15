using E_Commerce_Final_Demo.Data;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Reposatories.Implmentation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            if(entity is null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
                _dbSet.Remove(item);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            var items =  await _dbSet.ToListAsync();
            return items;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
            return item;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var item =  await _dbSet.FindAsync(id);
            if (item is  null)
                throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");


            _context.Entry(item).CurrentValues.SetValues(entity);
            

        }
    }
}
