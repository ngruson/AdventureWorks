using System.Data.Entity.ModelConfiguration;
using Entities = AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Entities.Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(p => p.Id);

            Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}