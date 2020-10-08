﻿using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductDocumentConfiguration : IEntityTypeConfiguration<ProductDocument>
    {
        public void Configure(EntityTypeBuilder<ProductDocument> builder)
        {
            builder.ToTable("Production.ProductDocument");
            builder.HasKey(pd => new { pd.Id, pd.DocumentNode });

            builder.Property(pd => pd.Id)
                .HasColumnName("ProductID");
        }
    }
}