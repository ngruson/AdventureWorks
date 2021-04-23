using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class IndividualCustomerConfiguration : EntityTypeConfiguration<Domain.IndividualCustomer>
    {
        public IndividualCustomerConfiguration()
        {
            ToTable("IndividualCustomer");
            HasKey(p => p.Id);
        }
    }
}