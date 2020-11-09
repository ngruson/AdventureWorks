using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer.UpdateCustomer
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}