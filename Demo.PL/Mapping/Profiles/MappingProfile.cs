﻿using AutoMapper;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Dtos.Employees;
using Demo.PL.ViewModels.Departments;

namespace Demo.PL.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department Module
            CreateMap<DepartmentViewModel, DepartmentToCreateDto>();
            CreateMap<DepartmentDetailesToReturnDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, DepartmentToUpdateDto>();
            #endregion

            #region Employee Module
            CreateMap<UpdateEmployeeDto, CreateEmployeeDto>();
            CreateMap<EmployeeDetailesDto, UpdateEmployeeDto>();
            #endregion
        }
    }
}
