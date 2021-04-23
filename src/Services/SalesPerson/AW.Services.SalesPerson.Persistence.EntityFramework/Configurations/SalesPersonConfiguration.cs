using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesPerson.Persistence.EntityFramework.Configurations
{
    public class SalesPersonConfiguration : EntityTypeConfiguration<Domain.SalesPerson>
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