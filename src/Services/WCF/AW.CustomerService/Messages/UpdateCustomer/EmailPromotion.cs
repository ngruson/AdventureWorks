using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/UpdateCustomer")]
    public enum EmailPromotion
    {
        NoPromotions = 0,
        AWPromotions = 1,
        AWAndPartnerPromotions = 2
    }
}