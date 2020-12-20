using System.ComponentModel.DataAnnotations;

namespace AW.Core.Application.Customer.GetCustomers
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}