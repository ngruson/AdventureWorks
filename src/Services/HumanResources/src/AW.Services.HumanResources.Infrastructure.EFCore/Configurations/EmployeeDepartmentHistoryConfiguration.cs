using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<Core.Entities.EmployeeDepartmentHistory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.EmployeeDepartmentHistory> builder)
        {
            builder.ToTable("EmployeeDepartmentHistory");
            builder.HasKey(_ => new { 
                _.EmployeeID, 
                _.DepartmentID,
                _.ShiftID,
                _.StartDate 
            });

            builder.Property(_ => _.EmployeeID)
                .ValueGeneratedNever();

            builder.Property(_ => _.DepartmentID)
                .ValueGeneratedNever();

            builder.Property(_ => _.ShiftID)
                .ValueGeneratedNever();
        }
    }
}