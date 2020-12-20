using AW.Core.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "Sales");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(c => c.StoreID)
                .HasColumnName("StoreID");

            builder.HasMany(e => e.SalesOrders)
                .WithOne(e => e.Customer)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}