using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<Core.Entities.EmployeeDepartmentHistory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.EmployeeDepartmentHistory> builder)
        {
            builder.ToTable("EmployeeDepartmentHistory");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("EmployeeDepartmentHistoryID");

            builder.Property(_ => _.ObjectId)
                .HasDefaultValue();
        }
    }
}
