using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Persistence.EntityFramework.Configurations
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<Domain.AddressType>
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