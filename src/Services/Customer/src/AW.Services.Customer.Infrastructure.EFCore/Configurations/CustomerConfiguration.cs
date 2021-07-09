using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities = AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Entities.Customer> builder)
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