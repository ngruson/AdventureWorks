﻿using System.Xml.Serialization;

namespace AW.CustomerService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class GetCustomerRequest
    {
        public string AccountNumber { get; set; }
    }
}