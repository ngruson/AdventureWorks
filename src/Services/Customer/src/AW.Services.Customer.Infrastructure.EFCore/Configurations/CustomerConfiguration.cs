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
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("CustomerID");

            builder.Property(_ => _.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}