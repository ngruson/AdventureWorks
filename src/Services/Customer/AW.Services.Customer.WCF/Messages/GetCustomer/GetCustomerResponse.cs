using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class GetCustomerResponse
    {
        public Customer Customer { get; set; }
    }
}