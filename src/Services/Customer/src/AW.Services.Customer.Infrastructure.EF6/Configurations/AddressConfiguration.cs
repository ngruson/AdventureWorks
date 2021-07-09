using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Address");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("AddressID");
        }
    }
}