using Demo.BLL.Dtos.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repostories.Departmemts;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        //private readonly IDepartmentRepository _departmentRepository;

        //public DepartmentService(IDepartmentRepository departmentRepository)
        //{
        //    _departmentRepository = departmentRepository;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentToReturnDto> GetDepartments()
        {
            //var departments = _departmentRepository.GetAll();
            //foreach (var department in departments)
            //{
            //    yield return new DepartmentToReturnDto
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Description = department.Description,
            //        Code = department.Code,
            //        CreationDate = department.CreationDate
            //    };
            //}
            var departments = _unitOfWork.DepartmentRepository.GetAllQueryable().Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Name = department.Name,
                //Description = department.Description,
                Code = department.Code,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToList();
            return departments;
        }
        public DepartmentDetailesToReturnDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetByID(id);
            if (department is not null)
            {
                return new DepartmentDetailesToReturnDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                    LastModifiedBy = department.LastModifiedBy,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedOn = department.LastModifiedOn,
                    IsDeleted = department.IsDeleted
                };
            }
            return null;
        }
        public int CreateDepartment(DepartmentToCreateDto entity)
        {
            var department = new Department()
            {
                Name = entity.Name,
                Description = entity.Description,
                Code = entity.Code,
                CreationDate = entity.CreationDate,
                CreatedBy = 1, //UserId
                LastModifiedBy = 1, //UserId
                LastModifiedOn = DateTime.UtcNow
            };
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.Complete();
        }
        public int UpdateDepartment(DepartmentToUpdateDto entity)
        {
            var department = new Department()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Code = entity.Code,
                CreationDate = entity.CreationDate,
                CreatedBy = 1, //UserId
                LastModifiedBy = 1, //UserId
                LastModifiedOn = DateTime.UtcNow
            };
            _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var departmentRepository = _unitOfWork.DepartmentRepository;
            var department = departmentRepository.GetByID(id);
            if(department is not null)
                departmentRepository.Delete(department);
            return _unitOfWork.Complete() > 0;
        }
    }
}
