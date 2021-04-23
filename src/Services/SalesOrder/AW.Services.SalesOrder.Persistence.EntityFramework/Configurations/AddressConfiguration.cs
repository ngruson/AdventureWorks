using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class AddressConfiguration : EntityTypeConfiguration<Domain.Address>
    {
        public AddressConfiguration()
        {
            ToTable("Address");
            HasKey(p => p.Id);
            Property(s => s.Id)
                .HasColumnName("AddressID");
        }
    }
}