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
        public IEnumerable<T> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _dbContext.Set<T>().AsNoTracking().ToList();
            return _dbContext.Set<T>().ToList();
        }
        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>();
        }
        public T? GetByID(int id)
        {
            //return _dbContext.T.Local.FirstOrDefault(D => D.Id == id);
            return _dbContext.Set<T>().Find(id);
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

        public IEnumerable<T> GetAllEnumerable()
        {
            return _dbContext.Set<T>();
        }
    }
}
