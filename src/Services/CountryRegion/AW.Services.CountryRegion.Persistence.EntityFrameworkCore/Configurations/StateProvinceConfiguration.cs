using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.Services.CountryRegion.Persistence.EntityFrameworkCore.Configurations
{
    public class StateProvinceConfiguration : IEntityTypeConfiguration<Domain.StateProvince>
    {
        public void Configure(EntityTypeBuilder<Domain.StateProvince> builder)
        {
            builder.ToTable("Person.StateProvince");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .HasColumnName("StateProvinceID");

            builder.Property(sp => sp.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}