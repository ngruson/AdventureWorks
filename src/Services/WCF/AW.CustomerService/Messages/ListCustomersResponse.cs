using AW.Application.GetCustomers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class ListCustomersResponse
    {
        [XmlElement(Namespace = "http://services.aw.com/CustomerService/1.0/ListCustomers")]
        public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
        public int TotalCustomers { get; set; }
    }
}