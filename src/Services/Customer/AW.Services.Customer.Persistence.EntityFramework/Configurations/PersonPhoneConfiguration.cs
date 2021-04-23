using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class PersonPhoneConfiguration : EntityTypeConfiguration<Domain.PersonPhone>
    {
        public PersonPhoneConfiguration()
        {
            ToTable("PersonPhone");
            HasKey(p => p.Id);
        }
    }
}