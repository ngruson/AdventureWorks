using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class IndividualCustomerConfiguration : EntityTypeConfiguration<Domain.IndividualCustomer>
    {
        public IndividualCustomerConfiguration()
        {
            ToTable("IndividualCustomer");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("CustomerID");
        }
    }
}