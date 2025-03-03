using Demo.DAL.Entities;

namespace Demo.DAL.Presistance.Repostories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool AsNoTracking = true);
        IQueryable<T> GetAllQueryable();
        T? GetByID(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
