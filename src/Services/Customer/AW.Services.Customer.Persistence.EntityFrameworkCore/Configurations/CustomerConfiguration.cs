using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer.Domain.Customer>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CustomerID");

            builder.Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}