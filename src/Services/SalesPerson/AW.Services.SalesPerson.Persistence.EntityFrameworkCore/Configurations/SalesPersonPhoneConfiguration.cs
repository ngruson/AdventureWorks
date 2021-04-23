using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesPerson.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesPersonPhoneConfiguration : IEntityTypeConfiguration<Domain.SalesPersonPhone>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesPersonPhone> builder)
        {
            builder.ToTable("SalesPersonPhone");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("SalesPersonPhoneID");
        }
    }
}