using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            ToTable("Document");
            HasKey(d => d.DocumentNode);

            Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(400);

            Property(d => d.FileExtension)
                .IsRequired()
                .HasMaxLength(8);

            Property(d => d.Revision)
                .IsRequired()
                .HasMaxLength(5);

            Property(d => d.ChangeNumber)
                .IsRequired();

            Property(d => d.Status)
                .IsRequired();
        }
    }
}