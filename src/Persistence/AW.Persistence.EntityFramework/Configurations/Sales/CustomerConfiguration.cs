using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Sales.Customer");

            Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            
            HasMany(e => e.SalesOrders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}