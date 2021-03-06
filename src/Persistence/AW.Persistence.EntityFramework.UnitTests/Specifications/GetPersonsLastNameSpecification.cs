﻿using Ardalis.Specification;
using AW.Core.Domain.Person;

namespace AW.Persistence.EntityFramework.UnitTests.Specifications
{
    public class GetPersonsLastNameSpecification : Specification<Person, string>
    {

        public GetPersonsLastNameSpecification()
        {
            Query.Select(p => p.LastName);
        }
    }
}