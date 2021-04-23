using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<Domain.CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<Domain.CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddress");
            builder.HasKey(p => p.Id);
        }
    }
}