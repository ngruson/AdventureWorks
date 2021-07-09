using AW.Services.SalesOrder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Infrastructure.EFCore.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(p => p.Id);
            builder.Property(s => s.Id)
                .HasColumnName("AddressID");
        }
    }
}