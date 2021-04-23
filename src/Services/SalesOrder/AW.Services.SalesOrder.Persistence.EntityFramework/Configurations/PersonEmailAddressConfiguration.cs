using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class PersonEmailAddressConfiguration : EntityTypeConfiguration<Domain.PersonEmailAddress>
    {
        public PersonEmailAddressConfiguration()
        {
            ToTable("PersonEmailAddress");
            HasKey(p => p.Id);
        }
    }
}