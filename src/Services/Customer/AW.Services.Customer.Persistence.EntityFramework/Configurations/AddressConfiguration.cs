using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class AddressConfiguration : EntityTypeConfiguration<Domain.Address>
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