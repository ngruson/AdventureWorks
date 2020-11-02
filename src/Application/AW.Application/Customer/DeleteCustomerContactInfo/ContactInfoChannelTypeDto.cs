using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer.DeleteCustomerContactInfo
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}