using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class LocationConfiguration : EntityTypeConfiguration<Domain.Location>
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