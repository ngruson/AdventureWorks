using AW.Core.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class StateProvinceConfiguration : EntityTypeConfiguration<StateProvince>
    {
        public StateProvinceConfiguration()
        {
            ToTable("Person.StateProvince");
            HasKey(sp => sp.Id);

            Property(sp => sp.Id)
                .HasColumnName("StateProvinceID");

            Property(sp => sp.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.SalesTerritoryID)
              .HasColumnName("TerritoryID");
        }
    }
}