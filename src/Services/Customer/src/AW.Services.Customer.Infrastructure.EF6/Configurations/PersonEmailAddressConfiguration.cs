using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class PersonEmailAddressConfiguration : EntityTypeConfiguration<PersonEmailAddress>
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