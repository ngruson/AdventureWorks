using AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders;
using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders
{
    public class UpdateProductViewModelBinder : ViewModelModelBinder<UpdateProductViewModel>
    {
        protected override string GetValueForKey(string key, StringValues value)
        {
            if (key == nameof(UpdateProductViewModel.Product.MakeFlag) ||
                key == nameof(UpdateProductViewModel.Product.FinishedGoodsFlag))
            {
                var stringValue = value.ToString();
                if (stringValue.Contains(','))
                {
                    return stringValue[..stringValue.IndexOf(',')];
                }
            }

            return base.GetValueForKey(key, value);
        }
    }
}
