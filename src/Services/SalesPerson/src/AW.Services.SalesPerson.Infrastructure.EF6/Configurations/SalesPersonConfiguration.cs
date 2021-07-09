using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesPerson.Infrastructure.EF6.Configurations
{
    public class SalesPersonConfiguration : EntityTypeConfiguration<Core.Entities.SalesPerson>
    {
        public SalesPersonConfiguration()
        {
            ToTable("SalesPerson");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("SalesPersonID");
        }
    }
}