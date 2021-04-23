using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Domain.Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("CustomerID");

            Property(c => c.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}