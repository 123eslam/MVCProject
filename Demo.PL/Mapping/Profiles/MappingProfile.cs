using AutoMapper;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Dtos.Projects;
using Demo.BLL.Dtos.WorkOn;
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
            CreateMap<EmployeeDetailesDto, UpdateEmployeeDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToIFormFile(src.Image)));

            #endregion

            #region Project Module
            CreateMap<UpdateProjectDto, CreateProjectDto>();
            CreateMap<ProjectDetailsDto, UpdateProjectDto>();
            #endregion

            #region Work on project
            CreateMap<UpdateEmployeeWorkOnProjectDto, AssignEmployeeWorkOnProjectDto>();
            CreateMap<EmployeeWorkOnProjectDetailsDto, UpdateEmployeeWorkOnProjectDto>();
            #endregion
        }
        private static IFormFile? ConvertToIFormFile(string? image)
        {
            if (string.IsNullOrEmpty(image))
                return null;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\images", image);
            var fileBytes = File.ReadAllBytes(filePath);
            var stream = new MemoryStream(fileBytes);

            return new FormFile(stream, 0, stream.Length, "file", image);
        }
    }
}
