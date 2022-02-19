﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<Core.Entities.SalesOrder>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");
            builder.HasKey("Id");
            
            builder.Property("Id")
                .HasColumnName("SalesOrderID");
            
            builder.Property(p => p.SalesOrderNumber)
                .HasComputedColumnSql();

            builder.Ignore(p => p.SubTotal);
            builder.Ignore(p => p.TaxAmt);
            builder.Ignore(p => p.TaxRate);
            builder.Ignore(p => p.TotalDue);
        }
    }
}