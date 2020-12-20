using AW.Core.Domain.HumanResources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class EmployeeDepartmentHistoryConfiguration : EntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public EmployeeDepartmentHistoryConfiguration()
        {
            ToTable("HumanResources.EmployeeDepartmentHistory");
            HasKey(edp => new { edp.BusinessEntityID, edp.DepartmentID, edp.ShiftID, edp.StartDate });

            Property(edp => edp.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(edp => edp.DepartmentID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(edp => edp.ShiftID)
                .HasColumnOrder(2);

            Property(edp => edp.StartDate)
                .HasColumnOrder(3)
                .HasColumnType("date");

            Property(edp => edp.EndDate)
                .HasColumnType("date");
        }
    }
}