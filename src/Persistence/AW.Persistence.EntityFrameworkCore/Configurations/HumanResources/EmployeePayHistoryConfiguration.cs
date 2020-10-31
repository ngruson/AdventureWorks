using AW.Domain.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.HumanResources
{
    public class EmployeePayHistoryConfiguration : IEntityTypeConfiguration<EmployeePayHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeePayHistory> builder)
        {
            builder.ToTable("HumanResources.EmployeePayHistory");
            builder.HasKey(eph => new { eph.BusinessEntityID, eph.RateChangeDate });

            builder.Property(eph => eph.BusinessEntityID)
                .ValueGeneratedNever();

            builder.Property(eph => eph.Rate)
                .HasColumnType("money");
        }
    }
}