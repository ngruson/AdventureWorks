using AW.Core.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class PersonCreditCardConfiguration : IEntityTypeConfiguration<PersonCreditCard>
    {
        public void Configure(EntityTypeBuilder<PersonCreditCard> builder)
        {
            builder.ToTable("PersonCreditCard", "Sales");
            builder.HasKey(pcc => new { pcc.BusinessEntityID, pcc.CreditCardID });

            builder.Property(pcc => pcc.BusinessEntityID)
                .ValueGeneratedNever();

            builder.Property(pcc => pcc.CreditCardID)
                .ValueGeneratedNever();
        }
    }
}