﻿using Demo.BLL.Common.Services.GetUsereLogin;
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
        private readonly IGetuserLogin _getuserLogin;

        public DepartmentService(IUnitOfWork unitOfWork,IGetuserLogin getuserLogin)
        {
            _unitOfWork = unitOfWork;
            _getuserLogin = getuserLogin;
        }
        public async Task<IEnumerable<DepartmentToReturnDto>> GetDepartmentsAsync()
        {
            #region Old Away
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
            #endregion

            var departments = await _unitOfWork.DepartmentRepository.GetAllQueryable().Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Name = department.Name,
                //Description = department.Description,
                Code = department.Code,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToListAsync();
            return departments;
        }
        public async Task<DepartmentDetailesToReturnDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
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
        public async Task<int> CreateDepartmentAsync(DepartmentToCreateDto entity)
        {
            var department = new Department()
            {
                Name = entity.Name,
                Description = entity.Description,
                Code = entity.Code,
                CreationDate = entity.CreationDate,
                CreatedBy = await _getuserLogin.GetUserNameLoginAsync(), //UserId
                LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(), //UserId
                LastModifiedOn = DateTime.UtcNow
            };
            _unitOfWork.DepartmentRepository.Add(department);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto entity)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(entity.Id);
            if (department is null)
                return 0;
            department.Name = entity.Name;
            department.Description = entity.Description;
            department.Code = entity.Code;
            department.CreationDate = entity.CreationDate;
            department.LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(); //UserId
            department.LastModifiedOn = DateTime.UtcNow;
            //var department = new Department()
            //{
            //    Id = entity.Id,
            //    Name = entity.Name,
            //    Description = entity.Description,
            //    Code = entity.Code,
            //    CreationDate = entity.CreationDate,
            //    LastModifiedBy = await _getuserLogin.GetUserNameLoginAsync(), //UserId
            //    LastModifiedOn = DateTime.UtcNow
            //};
            _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepository =  _unitOfWork.DepartmentRepository;
            var department = await departmentRepository.GetByIdAsync(id);
            if(department is not null)
                departmentRepository.Delete(department);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
