using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesPerson.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesPersonEmailAddressConfiguration : IEntityTypeConfiguration<Domain.SalesPersonEmailAddress>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesPersonEmailAddress> builder)
        {
            builder.ToTable("SalesPersonEmailAddress");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("SalesPersonEmailAddressID");
        }
    }
}