using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class IndividualCustomerConfiguration : EntityTypeConfiguration<IndividualCustomer>
    {
        public IndividualCustomerConfiguration()
        {
            ToTable("IndividualCustomer");
            HasKey(p => p.Id);
        }
    }
}