using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Persistence.EntityFrameworkCore.Configurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<Domain.AddressType>
    {
        public void Configure(EntityTypeBuilder<Domain.AddressType> builder)
        {
            builder.ToTable("AddressType");
            builder.HasKey(a => a.Id);

            builder.Property(at => at.Id)
                .HasColumnName("AddressTypeID");

            builder.Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}