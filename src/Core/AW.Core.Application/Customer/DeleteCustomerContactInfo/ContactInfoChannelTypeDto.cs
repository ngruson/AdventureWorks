using System.ComponentModel.DataAnnotations;

namespace AW.Core.Application.Customer.DeleteCustomerContactInfo
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}