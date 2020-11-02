using System.ComponentModel.DataAnnotations;

namespace AW.Application.Customer
{
    public enum ContactInfoChannelTypeDto
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}