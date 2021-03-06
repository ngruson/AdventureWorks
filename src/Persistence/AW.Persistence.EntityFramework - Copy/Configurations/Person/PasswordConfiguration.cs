﻿using AW.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class PasswordConfiguration : EntityTypeConfiguration<Password>
    {
        public PasswordConfiguration()
        {
            ToTable("Person.Password");
            HasKey(p => p.BusinessEntityID);

            Property(p => p.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false);

            Property(p => p.PasswordSalt)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}