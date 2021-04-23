using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SpecialOfferProductConfiguration : EntityTypeConfiguration<Domain.SpecialOfferProduct>
    {
        public SpecialOfferProductConfiguration()
        {
            ToTable("SpecialOfferProduct");
            HasKey(p => p.Id);
        }
    }
}