using AW.Services.ReferenceData.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Infrastructure.EF6.Configurations
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            ToTable("AddressType");
            HasKey(at => at.Id);

            Property(at => at.Id)
                .HasColumnName("AddressTypeID");

            Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}