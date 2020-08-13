﻿using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Person.Address");

            Property(a => a.AddressLine1)
                .IsRequired()
                .HasMaxLength(60);

            Property(a => a.AddressLine2)
                .HasMaxLength(60);

            Property(a => a.City)
                .IsRequired()
                .HasMaxLength(30);

            Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}