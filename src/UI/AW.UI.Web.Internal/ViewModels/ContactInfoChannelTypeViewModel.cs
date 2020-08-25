using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels
{
    public enum ContactInfoChannelTypeViewModel
    {
        [Display(Name = "E-mail")]
        Email,

        [Display(Name = "Phone")]
        Phone
    }
}