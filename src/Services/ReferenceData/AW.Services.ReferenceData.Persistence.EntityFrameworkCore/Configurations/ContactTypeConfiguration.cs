using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Persistence.EntityFrameworkCore.Configurations
{
    public class ContactTypeConfiguration : IEntityTypeConfiguration<Domain.ContactType>
    {
        public void Configure(EntityTypeBuilder<Domain.ContactType> builder)
        {
            builder.ToTable("ContactType");
            builder.HasKey(a => a.Id);

            builder.Property(at => at.Id)
                .HasColumnName("ContactTypeID");

            builder.Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}