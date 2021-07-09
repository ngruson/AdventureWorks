﻿using System.Collections.Generic;

namespace AW.Services.Customer.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; } = new List<PersonEmailAddress>();
        public List<PersonPhone> PhoneNumbers { get; set; } = new List<PersonPhone>();
    }
}