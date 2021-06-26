using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class StoreCustomerContactConfiguration : IEntityTypeConfiguration<Domain.StoreCustomerContact>
    {
        public void Configure(EntityTypeBuilder<Domain.StoreCustomerContact> builder)
        {
            builder.ToTable("StoreCustomerContact");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("StoreCustomerContactID");

            builder.Property(c => c.StoreCustomerId)
                .HasColumnName("CustomerID");

            builder.Property(c => c.ContactPersonId)
                .HasColumnName("PersonID");
        }
    }
}