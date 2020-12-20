using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            ToTable("Production.Location");

            Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(l => l.CostRate)
                .HasPrecision(10, 4)
                .HasColumnType("smallmoney");

            Property(l => l.Availability)
                .HasPrecision(8, 2);
        }
    }
}