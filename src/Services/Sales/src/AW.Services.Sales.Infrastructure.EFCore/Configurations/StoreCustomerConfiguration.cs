using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class StoreCustomerConfiguration : IEntityTypeConfiguration<StoreCustomer>
    {
        public void Configure(EntityTypeBuilder<StoreCustomer> builder)
        {
            builder.ToTable("StoreCustomer");
        }
    }
}