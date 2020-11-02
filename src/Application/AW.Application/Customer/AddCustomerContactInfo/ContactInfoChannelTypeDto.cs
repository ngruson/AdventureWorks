using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer.AddCustomerContactInfo
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}