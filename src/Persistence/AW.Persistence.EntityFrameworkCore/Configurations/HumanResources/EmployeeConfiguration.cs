using AW.Core.Domain.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.HumanResources
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NationalIDNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.LoginID)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.OrganizationLevel)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.BirthDate)
                .HasColumnType("date");

            builder.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.HireDate)
                .HasColumnType("date");

            builder.HasMany(e => e.EmployeeDepartmentHistory)
                .WithOne(e => e.Employee)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.EmployeePayHistory);

            builder.HasMany(e => e.PurchaseOrderHeaders)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}