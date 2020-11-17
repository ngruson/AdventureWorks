using AW.Domain.HumanResources;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            ToTable("HumanResources.Department");

            Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(d => d.GroupName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}