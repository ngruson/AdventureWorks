using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SpecialOfferConfiguration : EntityTypeConfiguration<Domain.SpecialOffer>
    {
        public SpecialOfferConfiguration()
        {
            ToTable("SpecialOffer");
            HasKey(p => p.Id);
        }
    }
}