using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class IllustrationConfiguration : EntityTypeConfiguration<Illustration>
    {
        public IllustrationConfiguration()
        {
            ToTable("Illustration");

            Property(i => i.Diagram)
                .HasColumnType("xml");
        }
    }
}