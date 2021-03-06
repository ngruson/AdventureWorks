﻿using AW.Core.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesOrderHeaderSalesReasonConfiguration : EntityTypeConfiguration<SalesOrderHeaderSalesReason>
    {
        public SalesOrderHeaderSalesReasonConfiguration()
        {
            ToTable("Sales.SalesOrderHeaderSalesReason");
            HasKey(sohsr => new { sohsr.SalesOrderID, sohsr.SalesReasonID });

            Property(sohsr => sohsr.SalesOrderID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sohsr => sohsr.SalesReasonID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}