using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SpecialOfferConfiguration : IEntityTypeConfiguration<SpecialOffer>
    {
        public void Configure(EntityTypeBuilder<SpecialOffer> builder)
        {
            builder.ToTable("SpecialOffer", "Sales");

            builder.Property(so => so.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(so => so.DiscountPct)
                .HasColumnType("decimal(10,4)");

            builder.Property(so => so.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(so => so.Category)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}