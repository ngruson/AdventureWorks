﻿using AW.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SpecialOfferConfiguration : EntityTypeConfiguration<SpecialOffer>
    {
        public SpecialOfferConfiguration()
        {
            ToTable("Sales.SpecialOffer");

            Property(so => so.Description)
                .IsRequired()
                .HasMaxLength(255);

            Property(so => so.DiscountPct)
                .HasPrecision(10, 4);

            Property(so => so.Type)
                .IsRequired()
                .HasMaxLength(50);

            Property(so => so.Category)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}