using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Domain.Location>
    {
        public void Configure(EntityTypeBuilder<Domain.Location> builder)
        {
            builder.ToTable("Location");

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.CostRate)
                .HasColumnType("decimal(10,4)")
                .HasColumnType("smallmoney");

            builder.Property(l => l.Availability)
                .HasColumnType("decimal(8,2)");
        }
    }
}