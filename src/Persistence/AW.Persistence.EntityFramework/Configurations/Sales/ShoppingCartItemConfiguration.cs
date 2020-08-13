using AW.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class ShoppingCartItemConfiguration : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemConfiguration()
        {
            ToTable("Sales.ShoppingCartItem");

            Property(sc => sc.ShoppingCartID)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
