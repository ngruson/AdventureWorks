using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesPerson.Persistence.EntityFramework.Configurations
{
    public class SalesPersonPhoneConfiguration : EntityTypeConfiguration<Domain.SalesPersonPhone>
    {
        public SalesPersonPhoneConfiguration()
        {
            ToTable("SalesPersonPhone");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("SalesPersonPhoneID");
        }
    }
}