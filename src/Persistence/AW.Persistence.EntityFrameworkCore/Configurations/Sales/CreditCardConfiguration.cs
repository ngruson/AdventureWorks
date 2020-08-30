using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCard", "Sales");

            builder.Property(cc => cc.CardType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cc => cc.CardNumber)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}