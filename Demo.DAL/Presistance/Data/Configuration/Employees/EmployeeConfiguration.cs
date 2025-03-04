using Demo.DAL.Common.Enums;
using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Data.Configuration.Employees
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.Address)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(8, 2)");

            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.Gender)
                .HasConversion(
                    (gender) => gender.ToString(),
                    (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );

            builder.Property(e => e.EmployeeType)
                .HasConversion(
                    (employeeType) => employeeType.ToString(),
                    (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType)
                );
        }
    }
}
