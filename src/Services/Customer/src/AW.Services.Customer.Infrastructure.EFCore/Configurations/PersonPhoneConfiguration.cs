﻿using AW.Services.Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class PersonPhoneConfiguration : IEntityTypeConfiguration<PersonPhone>
    {
        public void Configure(EntityTypeBuilder<PersonPhone> builder)
        {
            builder.ToTable("PersonPhone");
            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("PersonPhoneID");
        }
    }
}