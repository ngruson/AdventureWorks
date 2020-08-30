using AW.Domain.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.HumanResources
{
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> builder)
        {
            builder.ToTable("HumanResources.EmployeeDepartmentHistory");
            builder.HasKey(edp => new { edp.Id, edp.DepartmentID, edp.ShiftID, edp.StartDate });

            builder.Property(edp => edp.Id)
                .ValueGeneratedNever();

            builder.Property(edp => edp.DepartmentID)
                .ValueGeneratedNever();

            builder.Property(edp => edp.StartDate)
                .HasColumnType("date");

            builder.Property(edp => edp.EndDate)
                .HasColumnType("date");
        }
    }
}