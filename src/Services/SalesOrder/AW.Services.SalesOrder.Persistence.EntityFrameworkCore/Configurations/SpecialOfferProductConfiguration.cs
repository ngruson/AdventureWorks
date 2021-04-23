using AW.Services.SalesOrder.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SpecialOfferProductConfiguration : IEntityTypeConfiguration<SpecialOfferProduct>
    {
        public void Configure(EntityTypeBuilder<SpecialOfferProduct> builder)
        {
            builder.ToTable("SpecialOfferProduct");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("SpecialOfferProductID");
        }
    }
}