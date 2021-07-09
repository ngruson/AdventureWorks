using AW.Services.SalesOrder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Infrastructure.EFCore.Configurations
{
    public class SpecialOfferConfiguration : IEntityTypeConfiguration<SpecialOffer>
    {
        public void Configure(EntityTypeBuilder<SpecialOffer> builder)
        {
            builder.ToTable("SpecialOffer");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("SpecialOfferID");
        }
    }
}