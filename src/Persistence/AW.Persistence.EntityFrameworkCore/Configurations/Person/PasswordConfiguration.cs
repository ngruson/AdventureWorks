﻿using AW.Core.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.ToTable("Person.Password");
            builder.HasKey(p => p.BusinessEntityID);

            builder.Property(p => p.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();

            builder.Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(p => p.PasswordSalt)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}