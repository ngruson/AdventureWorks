using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SpecialOfferProductConfiguration : EntityTypeConfiguration<SpecialOfferProduct>
    {
        public SpecialOfferProductConfiguration()
        {
            ToTable("SpecialOfferProduct");
            HasKey(p => p.Id);
        }
    }
}