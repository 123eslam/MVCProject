using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostories.Departmemts
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetAll(bool AsNoTracking = true)
        {
            if(AsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();
            return _dbContext.Departments.ToList();
        }
        public IQueryable<Department> GetAllQueryable()
        {
            return _dbContext.Departments;
        }
        public Department? GetByID(int id)
        {
            //return _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            return _dbContext.Departments.Find(id);
        }
        public int AddDepartment(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int UpdateDepartment(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int DeleteDepartment(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
