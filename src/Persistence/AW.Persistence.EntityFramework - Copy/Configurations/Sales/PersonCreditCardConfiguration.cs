using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class PersonCreditCardConfiguration : EntityTypeConfiguration<PersonCreditCard>
    {
        public PersonCreditCardConfiguration()
        {
            ToTable("Sales.PersonCreditCard");
            HasKey(pcc => new { pcc.BusinessEntityID, pcc.CreditCardID });

            Property(pcc => pcc.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pcc => pcc.CreditCardID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}