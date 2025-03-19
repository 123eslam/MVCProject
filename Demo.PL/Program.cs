using Demo.BLL.Common.Services.Attachment_Services;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Presistance.Data.Context;
using Demo.DAL.Presistance.UnitOfWork;
using Demo.PL.Mapping.Profiles;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
