using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesReasonConfiguration : IEntityTypeConfiguration<SalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesReason> builder)
        {
            builder.ToTable("SalesReason");
            builder.HasKey(p => p.Id);
            builder.Property(s => s.Id)
                .HasColumnName("SalesReasonID");
        }
    }
}