using AW.Services.Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class StoreCustomerContactConfiguration : IEntityTypeConfiguration<StoreCustomerContact>
    {
        public void Configure(EntityTypeBuilder<StoreCustomerContact> builder)
        {
            builder.ToTable("StoreCustomerContact");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("StoreCustomerContactID");

            builder.Property(c => c.StoreCustomerId)
                .HasColumnName("CustomerID");

            builder.Property(c => c.ContactPersonId)
                .HasColumnName("PersonID");
        }
    }
}