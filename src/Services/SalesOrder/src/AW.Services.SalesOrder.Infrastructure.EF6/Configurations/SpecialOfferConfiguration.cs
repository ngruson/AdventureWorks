using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SpecialOfferConfiguration : EntityTypeConfiguration<SpecialOffer>
    {
        public SpecialOfferConfiguration()
        {
            ToTable("SpecialOffer");
            HasKey(p => p.Id);
        }
    }
}