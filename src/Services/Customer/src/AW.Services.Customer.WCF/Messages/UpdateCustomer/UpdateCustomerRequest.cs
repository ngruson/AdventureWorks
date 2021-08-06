using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class UpdateCustomerRequest
    {
        public Core.Models.UpdateCustomer.Customer Customer { get; set; }
    }
}