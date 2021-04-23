using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Persistence.EntityFramework.Configurations
{
    public class ContactTypeConfiguration : EntityTypeConfiguration<Domain.ContactType>
    {
        public ContactTypeConfiguration()
        {
            ToTable("ContactType");
            HasKey(at => at.Id);

            Property(at => at.Id)
                .HasColumnName("ContactTypeID");

            Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}