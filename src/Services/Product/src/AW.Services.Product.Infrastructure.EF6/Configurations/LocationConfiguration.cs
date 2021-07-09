using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            ToTable("Location");

            Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(l => l.CostRate)
                .HasColumnType("decimal(10,4)")
                .HasColumnType("smallmoney");

            Property(l => l.Availability)
                .HasPrecision(8, 2);
        }        
    }
}