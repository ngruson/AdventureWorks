using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
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