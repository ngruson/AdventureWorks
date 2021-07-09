using AW.Services.ReferenceData.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Infrastructure.EF6.Configurations
{
    public class ContactTypeConfiguration : EntityTypeConfiguration<ContactType>
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