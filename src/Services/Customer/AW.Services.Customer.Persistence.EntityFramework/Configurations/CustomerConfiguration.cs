using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Domain.Customer>
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