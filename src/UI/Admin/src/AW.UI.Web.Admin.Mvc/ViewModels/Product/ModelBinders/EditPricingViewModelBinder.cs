using AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;
using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders
{
    public class EditPricingViewModelBinder : ViewModelModelBinder<EditPricingViewModel>
    {
        protected override object GetValueForKey(string key, StringValues value)
        {
            if (key == nameof(EditPricingViewModel.Product.StandardCost))
            {
                var strValue = value.ToString();
                if (strValue.StartsWith('$'))
                    strValue = strValue[1..].Trim();

                if (decimal.TryParse(strValue, out var standardCost))
                    return standardCost;

                throw new ArgumentException($"Standard cost value {value} could not be parsed");
            }
            else if (key == nameof(EditPricingViewModel.Product.ListPrice))
            {
                var strValue = value.ToString();
                if (strValue.StartsWith('$'))
                    strValue = strValue[1..].Trim();

                if (decimal.TryParse(strValue, out var listPrice))
                    return listPrice;

                throw new ArgumentException($"List price value {value} could not be parsed");
            }

            return base.GetValueForKey(key, value);
        }
    }
}
