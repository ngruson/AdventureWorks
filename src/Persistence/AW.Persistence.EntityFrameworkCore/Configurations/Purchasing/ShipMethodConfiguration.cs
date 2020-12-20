using AW.Core.Domain.Purchasing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Purchasing
{
    public class ShipMethodConfiguration : IEntityTypeConfiguration<ShipMethod>
    {
        public void Configure(EntityTypeBuilder<ShipMethod> builder)
        {
            builder.ToTable("Purchasing.ShipMethod");
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Id)
                .HasColumnName("ShipMethodID");

            builder.Property(sm => sm.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sm => sm.ShipBase)
                .HasColumnType("money");

            builder.Property(sm => sm.ShipRate)
                .HasColumnType("money");
        }
    }
}