using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class IndividualCustomerConfiguration : IEntityTypeConfiguration<Customer.Domain.IndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.IndividualCustomer> builder)
        {
            builder.ToTable("IndividualCustomer");

            //builder.Property(c => c.PersonId)
            //    .HasColumnName("PersonID");

            //builder.HasOne(c => c.Person);

            //builder.Property(c => c.Person)
            //.HasColumnName("PersonID");

            //builder.Property(c => c.Id)
            //    .HasColumnName("IndividualCustomerID");
        }
    }
}