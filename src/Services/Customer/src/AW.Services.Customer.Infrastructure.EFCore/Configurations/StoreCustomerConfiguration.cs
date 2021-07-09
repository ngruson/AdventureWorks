using AW.Services.Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class StoreCustomerConfiguration : IEntityTypeConfiguration<StoreCustomer>
    {
        public void Configure(EntityTypeBuilder<StoreCustomer> builder)
        {
            builder.ToTable("StoreCustomer");
        }
    }
}