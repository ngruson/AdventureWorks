using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class CreditCardConfiguration : EntityTypeConfiguration<Domain.CreditCard>
    {
        public CreditCardConfiguration()
        {
            ToTable("CreditCard");
            HasKey(p => p.Id);
        }
    }
}