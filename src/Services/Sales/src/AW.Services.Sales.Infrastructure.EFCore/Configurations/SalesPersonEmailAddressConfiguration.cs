using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesPersonEmailAddressConfiguration : IEntityTypeConfiguration<SalesPersonEmailAddress>
    {
        public void Configure(EntityTypeBuilder<SalesPersonEmailAddress> builder)
        {
            builder.ToTable("SalesPersonEmailAddress");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("SalesPersonEmailAddressID");
        }
    }
}