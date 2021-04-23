using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesPerson.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesPersonConfiguration : IEntityTypeConfiguration<Domain.SalesPerson>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesPerson> builder)
        {
            builder.ToTable("SalesPerson");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("SalesPersonID");
        }
    }
}