using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductInventoryConfiguration : EntityTypeConfiguration<Domain.ProductInventory>
    {
        public ProductInventoryConfiguration()
        {
            ToTable("ProductInventory");
            HasKey(pic => new { pic.ProductID, pic.LocationID });

            Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}