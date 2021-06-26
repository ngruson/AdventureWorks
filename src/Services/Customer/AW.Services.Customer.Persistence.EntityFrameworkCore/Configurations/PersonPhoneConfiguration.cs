using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class PersonPhoneConfiguration : IEntityTypeConfiguration<Domain.PersonPhone>
    {
        public void Configure(EntityTypeBuilder<Domain.PersonPhone> builder)
        {
            builder.ToTable("PersonPhone");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("PersonPhoneID");
        }
    }
}