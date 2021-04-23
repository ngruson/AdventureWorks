using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class PersonPhoneConfiguration : IEntityTypeConfiguration<Customer.Domain.PersonPhone>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.PersonPhone> builder)
        {
            builder.ToTable("PersonPhone");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("PersonPhoneID");
        }
    }
}