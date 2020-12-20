using System.ComponentModel.DataAnnotations;

namespace AW.Core.Application.Customer.AddCustomerContactInfo
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}