using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
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