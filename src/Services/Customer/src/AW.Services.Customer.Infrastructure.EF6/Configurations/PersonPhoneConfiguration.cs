using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class PersonPhoneConfiguration : EntityTypeConfiguration<PersonPhone>
    {
        public PersonPhoneConfiguration()
        {
            ToTable("PersonPhone");
            HasKey(p => p.Id);
        }
    }
}