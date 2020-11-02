﻿using Ardalis.Specification;

namespace AW.Application.Specifications
{
    public class GetPhoneNumberTypeSpecification : Specification<Domain.Person.PhoneNumberType>
    {
        public GetPhoneNumberTypeSpecification(string name) : base()
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}