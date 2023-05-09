using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Core.Entities.Shift>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.Shift> builder)
        {
            builder.ToTable("Shift");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("ShiftId");

            builder.Property(_ => _.ObjectId)
                .HasDefaultValue();
        }
    }
}
