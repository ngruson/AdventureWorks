using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class CreditCardConfiguration : EntityTypeConfiguration<CreditCard>
    {
        public CreditCardConfiguration()
        {
            ToTable("CreditCard");
            HasKey(p => p.Id);
        }
    }
}