﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductProductPhotoConfiguration : IEntityTypeConfiguration<Domain.ProductProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductProductPhoto> builder)
        {
            builder.ToTable("ProductProductPhoto");
            builder.HasKey(ppp => new { ppp.ProductId, ppp.ProductPhotoId });

            builder.Property(ppp => ppp.ProductId)
                .ValueGeneratedNever();

            builder.Property(ppp => ppp.ProductPhotoId)
                .ValueGeneratedNever();

            builder.HasOne(ppp => ppp.Product)
                .WithMany(p => p.ProductProductPhotos)
                .HasForeignKey("ProductId");
        }
    }
}