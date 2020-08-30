using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SpecialOfferProductConfiguration : IEntityTypeConfiguration<SpecialOfferProduct>
    {
        public void Configure(EntityTypeBuilder<SpecialOfferProduct> builder)
        {
            builder.ToTable("SpecialOfferProduct", "Sales");
            builder.HasKey(sop => new { sop.SpecialOfferID, sop.ProductID });

            builder.Property(sop => sop.SpecialOfferID)
                .ValueGeneratedNever();

            builder.Property(sop => sop.ProductID)
                .ValueGeneratedNever();
        }
    }
}