using AW.Core.Domain.HumanResources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("HumanResources.Employee");
            HasKey(e => e.Id);

            Property(e => e.NationalIDNumber)
                .IsRequired()
                .HasMaxLength(15);

            Property(e => e.LoginID)
                .IsRequired()
                .HasMaxLength(256);

            Property(e => e.OrganizationLevel)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.BirthDate)
                .HasColumnType("date");

            Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(1);

            Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(1);

            Property(e => e.HireDate)
                .HasColumnType("date");

            HasMany(e => e.EmployeeDepartmentHistory)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            HasMany(e => e.EmployeePayHistory);

            HasMany(e => e.PurchaseOrderHeaders)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);
        }
    }
}