using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class ShipMethodConfiguration : IEntityTypeConfiguration<ShipMethod>
    {
        public void Configure(EntityTypeBuilder<ShipMethod> builder)
        {
            builder.ToTable("ShipMethod");
            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("ShipMethodID");
        }
    }
}