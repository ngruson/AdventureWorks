using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer.GetCustomers
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}