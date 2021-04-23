using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class StoreCustomerConfiguration : IEntityTypeConfiguration<Domain.StoreCustomer>
    {
        public void Configure(EntityTypeBuilder<Domain.StoreCustomer> builder)
        {
            builder.ToTable("StoreCustomer");
        }
    }
}