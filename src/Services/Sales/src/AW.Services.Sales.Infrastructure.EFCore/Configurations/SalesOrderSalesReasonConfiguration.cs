﻿using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesOrderSalesReasonConfiguration : IEntityTypeConfiguration<SalesOrderSalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesOrderSalesReason> builder)
        {
            builder.ToTable("SalesOrderSalesReason");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("SalesOrderSalesReasonID");
        }
    }
}