using AW.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesTerritoryConfiguration : EntityTypeConfiguration<SalesTerritory>
    {
        public SalesTerritoryConfiguration()
        {
            ToTable("Sales.SalesTerritory");
            HasKey(st => st.Id);

            Property(st => st.Id)
                .HasColumnName("TerritoryID");

            Property(st => st.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(st => st.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(st => st.Group)
                .IsRequired()
                .HasMaxLength(50);

            Property(st => st.SalesYTD)
                 .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(st => st.SalesLastYear)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(st => st.CostYTD)
                .HasPrecision(19, 4)
                .HasColumnType("money");
                
            Property(st => st.CostLastYear)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            HasMany(e => e.StateProvinces)
                .WithRequired(e => e.SalesTerritory)
                .WillCascadeOnDelete(false);

            HasMany(e => e.SalesTerritoryHistory)
                .WithRequired(e => e.SalesTerritory)
                .WillCascadeOnDelete(false);
        }
    }
}