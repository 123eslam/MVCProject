using Demo.BLL.Common.Services.Attachment_Services;
using Demo.BLL.Dtos.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Repostories.Employees;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        //private readonly IEmployeeRepository _employeeRepository;

        //public EmployeeService(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork ,IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }
        public IEnumerable<EmployeeDto> GetEmployees(string SearchValue)
        {
            var query = _unitOfWork.EmployeeRepository.GetAllQueryable()
                .Include(E => E.Department)
                .Where(E => (string.IsNullOrEmpty(SearchValue) || E.Name.ToLower().Contains(SearchValue.ToLower())))
                .Select(employee => new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    EmployeeType = employee.EmployeeType.ToString(),
                    Gender = employee.Gender.ToString(),
                    Image = employee.Image,
                    Department = employee.Department.Name
                });
            //var employees = query.ToList();
            //var count = query.Count();
            //var employee = query.FirstOrDefault();
            return query;
        }
        public EmployeeDetailesDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetByID(id);
            if (employee is { })
                return new EmployeeDetailesDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    Department = employee.Department?.Name,
                    Image = employee.Image,
                    DepartmentId = employee.DepartmentId
                };
            return null;
        }
        public int CreateEmployee(CreateEmployeeDto entity)
        {
            var employee = new Employee()
            {
                Name = entity.Name,
                Age = entity.Age,
                Address = entity.Address,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                HiringDate = entity.HiringDate,
                Gender = entity.Gender,
                EmployeeType = entity.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = entity.DepartmentId
            };
            if (entity.Image is not null)
                employee.Image = _attachmentService.Upload(entity.Image, "images");
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.Complete();
        }
        public int UpdateEmployee(UpdateEmployeeDto entity)
        {
            var employee = new Employee()
            {
                Id = entity.Id,
                Name = entity.Name,
                Age = entity.Age,
                Address = entity.Address,
                Salary = entity.Salary,
                IsActive = entity.IsActive,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                HiringDate = entity.HiringDate,
                Gender = entity.Gender,
                EmployeeType = entity.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = entity.DepartmentId
            };
            if (entity.Image is not null)
                employee.Image = _attachmentService.Upload(entity.Image, "images");
            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.Complete();
        }
        public bool DeleteEmployee(int id)
        {
            var employeeRepository = _unitOfWork.EmployeeRepository;
            var employee = employeeRepository.GetByID(id);
            if (employee is { })
            {
                employeeRepository.Delete(employee);
                if (employee.Image is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\images", employee.Image);
                    _attachmentService.Delete(filePath);
                }
            }
            return _unitOfWork.Complete() > 0;
        }
    }
}
