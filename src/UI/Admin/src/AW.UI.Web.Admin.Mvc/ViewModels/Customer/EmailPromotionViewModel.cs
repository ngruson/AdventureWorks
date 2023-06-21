using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public enum EmailPromotionViewModel
{

    [Display(Name = "Contact does not wish to receive e-mail promotions")]
    NoPromotions,

    [Display(Name = "Contact does wish to receive e-mail promotions from AdventureWorks")]
    AWPromotions,

    [Display(Name = "Contact does wish to receive e-mail promotions from AdventureWorks and selected partners")]
    AWAndPartnerPromotions
}
