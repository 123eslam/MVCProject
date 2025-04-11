using Demo.DAL.Entities;

namespace Demo.DAL.Presistance.Repostories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);
        IQueryable<T> GetAllQueryable();
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
