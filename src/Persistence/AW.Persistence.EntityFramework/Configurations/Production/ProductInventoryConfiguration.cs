using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductInventoryConfiguration : EntityTypeConfiguration<ProductInventory>
    {
        public ProductInventoryConfiguration()
        {
            ToTable("Production.ProductInventory");
            HasKey(pic => new { pic.ProductID, pic.LocationID });

            Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}