using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class IndividualCustomerConfiguration : IEntityTypeConfiguration<Domain.IndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<Domain.IndividualCustomer> builder)
        {
            builder.ToTable("IndividualCustomer");
        }
    }
}