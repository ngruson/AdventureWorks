using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer.GetCustomer
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}