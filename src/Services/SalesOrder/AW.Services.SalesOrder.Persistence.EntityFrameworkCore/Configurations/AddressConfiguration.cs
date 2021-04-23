using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Domain.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(p => p.Id);
            builder.Property(s => s.Id)
                .HasColumnName("AddressID");
        }
    }
}