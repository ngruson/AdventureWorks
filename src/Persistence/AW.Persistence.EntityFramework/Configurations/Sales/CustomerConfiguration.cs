using AW.Core.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Sales.Customer");
            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasColumnName("CustomerID");

            Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(c => c.StoreID)
                .HasColumnName("StoreID");

            Property(c => c.SalesTerritoryID)
              .HasColumnName("TerritoryID");

            HasMany(e => e.SalesOrders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}