using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesPerson.Persistence.EntityFramework.Configurations
{
    public class SalesPersonEmailAddressConfiguration : EntityTypeConfiguration<Domain.SalesPersonEmailAddress>
    {
        public SalesPersonEmailAddressConfiguration()
        {
            ToTable("SalesPersonEmailAddress");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("SalesPersonEmailAddressID");
        }
    }
}