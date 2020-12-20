using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CreditCardConfiguration : EntityTypeConfiguration<CreditCard>
    {
        public CreditCardConfiguration()
        {
            ToTable("Sales.CreditCard");

            Property(cc => cc.CardType)
                .IsRequired()
                .HasMaxLength(50);

            Property(cc => cc.CardNumber)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}