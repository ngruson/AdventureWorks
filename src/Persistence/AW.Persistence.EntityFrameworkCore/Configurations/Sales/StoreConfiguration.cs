using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store", "Sales");

            builder.Property(s => s.Name)
                .HasMaxLength(50);

            builder.Property(s => s.Demographics)
                .HasColumnType("xml");

            builder.Property(s => s.SalesPersonID)
                .HasColumnName("SalesPersonID");
        }
    }
}