﻿using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetContactTypeSpecification : Specification<Domain.Person.ContactType>
    {
        public GetContactTypeSpecification(string name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}