using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
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