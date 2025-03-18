using Demo.DAL.Entities;

namespace Demo.DAL.Presistance.Repostories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool AsNoTracking = true);
        IQueryable<T> GetAllQueryable();
        IEnumerable<T> GetAllEnumerable();
        T? GetByID(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
