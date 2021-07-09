using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
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