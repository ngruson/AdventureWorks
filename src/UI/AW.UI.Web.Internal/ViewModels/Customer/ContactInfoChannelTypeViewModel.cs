using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public enum ContactInfoChannelTypeViewModel
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}