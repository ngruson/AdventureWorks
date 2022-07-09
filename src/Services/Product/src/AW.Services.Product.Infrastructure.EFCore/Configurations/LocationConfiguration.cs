using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Core.Entities.Location>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.CostRate)
                .HasColumnType("decimal(10,4)")
                .HasColumnType("smallmoney");

            builder.Property(_ => _.Availability)
                .HasColumnType("decimal(8,2)");
        }
    }
}