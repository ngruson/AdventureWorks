using System.Xml.Serialization;

namespace AW.CustomerService.Messages.GetCustomer
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/GetCustomer")]
    public enum EmailPromotion
    {
        NoPromotions = 0,
        AWPromotions = 1,
        AWAndPartnerPromotions = 2
    }
}