using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Core.Entities.Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("DepartmentId");

            builder.Property(_ => _.ObjectId)
                .HasDefaultValue();
        }
    }
}
