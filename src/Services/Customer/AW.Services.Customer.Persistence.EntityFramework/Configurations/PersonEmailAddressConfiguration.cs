using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class PersonEmailAddressConfiguration : EntityTypeConfiguration<Domain.PersonEmailAddress>
    {
        public PersonEmailAddressConfiguration()
        {
            ToTable("PersonEmailAddress");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("PersonEmailAddressID");
        }
    }
}