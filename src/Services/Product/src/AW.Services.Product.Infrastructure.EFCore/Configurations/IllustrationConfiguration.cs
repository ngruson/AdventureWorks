using AW.Services.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class IllustrationConfiguration : IEntityTypeConfiguration<Illustration>
    {
        public void Configure(EntityTypeBuilder<Illustration> builder)
        {
            builder.ToTable("Illustration");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Diagram)
                .HasColumnType("xml");
        }
    }
}