using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class StoreCustomerConfiguration : IEntityTypeConfiguration<Customer.Domain.StoreCustomer>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.StoreCustomer> builder)
        {
            builder.ToTable("StoreCustomer");

            //builder.Property(c => c.Id)
                //.HasColumnName("StoreCustomerID");
        }
    }
}