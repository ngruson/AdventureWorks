using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesPersonPhoneConfiguration : IEntityTypeConfiguration<SalesPersonPhone>
    {
        public void Configure(EntityTypeBuilder<SalesPersonPhone> builder)
        {
            builder.ToTable("SalesPersonPhone");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("SalesPersonPhoneID");
        }
    }
}