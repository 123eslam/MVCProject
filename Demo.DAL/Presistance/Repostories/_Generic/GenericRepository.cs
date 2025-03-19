using Demo.DAL.Entities;
using Demo.DAL.Presistance.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Repostories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase    
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }
        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            //return _dbContext.T.Local.FirstOrDefault(D => D.Id == id);
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
