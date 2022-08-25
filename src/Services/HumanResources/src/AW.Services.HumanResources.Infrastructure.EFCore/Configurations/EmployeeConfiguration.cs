using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Core.Entities.Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.Employee> builder)
        {
            builder.ToTable("Employee");

            builder.Property(_ => _.Salaried)
                .HasColumnName("SalariedFlag");

            builder.Property(_ => _.Current)
                .HasColumnName("CurrentFlag");
        }
    }
}