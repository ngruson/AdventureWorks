using AW.Domain.Purchasing;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Purchasing
{
    public class ShipMethodConfiguration : EntityTypeConfiguration<ShipMethod>
    {
        public ShipMethodConfiguration()
        {
            ToTable("Purchasing.ShipMethod");

            Property(sm => sm.Name)
                .IsRequired()
            .HasMaxLength(50);

            Property(sm => sm.ShipBase)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sm => sm.ShipRate)
                .HasPrecision(19, 4)
                .HasColumnType("money");               
        }
    }
}