using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CustomerID");

            builder.Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}