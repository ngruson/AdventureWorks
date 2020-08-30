using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Production.Document");
            builder.HasKey(d => d.DocumentNode);

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(d => d.FileExtension)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(d => d.Revision)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(d => d.ChangeNumber)
                .IsRequired();

            builder.Property(d => d.Status)
                .IsRequired();
        }
    }
}