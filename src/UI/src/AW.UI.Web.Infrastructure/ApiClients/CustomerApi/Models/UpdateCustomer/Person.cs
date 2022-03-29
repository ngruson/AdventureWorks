﻿using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class Person : IMapFrom<GetCustomer.Person>
    {
        public string Title { get; set; }
        public PersonName Name { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; }
        public List<PersonPhone> PhoneNumbers { get; set; }
    }
}